using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
using Services.Enums;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Services.Classes
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<OrderResponse> GetById(string orderKey)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: nameof(Order.OrderKey),  includeProperties: "CustomerInfoKeyNavigation,OrderDetails,OrderDetails.ProductKeyNavigation");
			if(order == null)
			{
				return null;
			}
			var response = _mapper.Map<OrderResponse>(order);
			response.CustomerInfo = _mapper.Map<CustomerInformationResponse>(order.CustomerInfoKeyNavigation);

			// Map OrderDetails to OrderDetailResponse and include Product information
			response.OrderDetails = order.OrderDetails.Select(orderDetail =>
			{
				var detailResponse = _mapper.Map<OrderDetailResponse>(orderDetail);
				detailResponse.Product = _mapper.Map<ProductResponse>(orderDetail.ProductKeyNavigation);
				return detailResponse;
			}).ToList();
			return response;
		}

		public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
		{
			var response = new OrderResponse();
			using (var transaction = await _unitOfWork.BeginTransactionAsync())
			{
				try
				{
					// Map and add Order
					var order = _mapper.Map<Order>(request);
					var orderKey = string.Format("{0}{1}", PrefixKeyConstant.ORDER, Guid.NewGuid().ToString().ToUpper());
					order.OrderKey = orderKey;
					order.CreatedAt = Utils.GetDateTimeNow();
					order.Status = (byte)OrderStatusEnum.Unpaid;
					_unitOfWork.OrderRepository.Add(order);
					await _unitOfWork.CommitAsync();

					// Map and add Customer Information
					var cusInfoReq = _mapper.Map<CustomerInformation>(request.CustomerInfo);
					cusInfoReq.CustomerInfoKey = string.Format("{0}{1}", PrefixKeyConstant.CUSTOMER_INFO, Guid.NewGuid().ToString().ToUpper());
					_unitOfWork.CustomerInformationRepository.Add(cusInfoReq);
					await _unitOfWork.CommitAsync();

					// Map and add Order Details
					var listOrderDetails = _mapper.Map<List<OrderDetail>>(request.Items);
					foreach (var orderDetail in listOrderDetails)
					{
						orderDetail.OrderKey = orderKey;
						orderDetail.ProductKeyNavigation = await _unitOfWork.ProductRepository.GetByIdAsync(orderDetail.ProductKey, keyColumn: "productKey");
						_unitOfWork.OrderDetailRepository.Add(orderDetail);
						await _unitOfWork.CommitAsync();
					}

					// Commit transaction if all operations succeed
					await transaction.CommitAsync();

					var res = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: "orderKey", includeProperties: "CustomerInfoKeyNavigation,OrderDetails,OrderDetails.ProductKeyNavigation");
					response = _mapper.Map<OrderResponse>(res);
					response.CustomerInfo = _mapper.Map<CustomerInformationResponse>(order.CustomerInfoKeyNavigation);

					// Map OrderDetails to OrderDetailResponse and include Product information
					response.OrderDetails = order.OrderDetails.Select(orderDetail =>
					{
						var detailResponse = _mapper.Map<OrderDetailResponse>(orderDetail);
						detailResponse.Product = _mapper.Map<ProductResponse>(orderDetail.ProductKeyNavigation);
						return detailResponse;
					}).ToList();
				}
				catch (Exception)
				{
					// Rollback transaction if any operation fails
					await transaction.RollbackAsync();
				}
				return response;
			}
		}


		public async Task<OrderResponse> UpdateOrder(string orderKey, UpdateOrderRequest request)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: "orderKey", includeProperties: "CustomerInfoKeyNavigation,OrderDetails,OrderDetails.ProductKeyNavigation");
			if (order == null) return null;

			_mapper.Map(request, order);
			_unitOfWork.OrderRepository.Update(order);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<OrderResponse>(order);
		}

		public async Task<bool> DeleteOrder(string orderKey)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: "orderKey");
			if (order == null) return false;

			_unitOfWork.OrderRepository.Delete(order);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<OrderResponse>> GetAllOrders(GetListOrderRequest request)
		{
			var orders = await _unitOfWork.OrderRepository.GetAllAsync(includeProperties: "CustomerInfoKeyNavigation,OrderDetails,OrderDetails.ProductKeyNavigation");

			// Map the list of orders to OrderResponse including related entities
			var orderResponses = orders.Select(order =>
			{
				var orderResponse = _mapper.Map<OrderResponse>(order);
				orderResponse.CustomerInfo = _mapper.Map<CustomerInformationResponse>(order.CustomerInfoKeyNavigation);

				// Map OrderDetails to OrderDetailResponse and include Product information
				orderResponse.OrderDetails = order.OrderDetails.Select(orderDetail =>
				{
					var detailResponse = _mapper.Map<OrderDetailResponse>(orderDetail);
					detailResponse.Product = _mapper.Map<ProductResponse>(orderDetail.ProductKeyNavigation);
					return detailResponse;
				}).ToList();

				return orderResponse;
			});

			return await orderResponses.ToPaginateAsync(request);
		}

	}
}
