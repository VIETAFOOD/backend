using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
using Services.Enums;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;

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
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: "orderKey", o => o.CustomerInfoKeyNavigation, o => o.OrderDetails);
			return order == null ? null : _mapper.Map<OrderResponse>(order);
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

					var res = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey, keyColumn: "orderKey", o => o.CustomerInfoKeyNavigation, o => o.OrderDetails);
					response = _mapper.Map<OrderResponse>(res);
					return _mapper.Map<OrderResponse>(order);
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
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey,keyColumn: "orderKey", o => o.CustomerInfoKeyNavigation, o => o.OrderDetails);
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
			var orders = await _unitOfWork.OrderRepository.GetAllAsync(
				o => o.CustomerInfoKeyNavigation, o => o.OrderDetails);
			var mapperList = _mapper.Map<IEnumerable<OrderResponse>>(orders);
			return await mapperList.ToPaginateAsync(request);
		}
	}
}
