using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Order;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
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
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey);
			return order == null ? null : _mapper.Map<OrderResponse>(order);
		}

		public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
		{
			var order = _mapper.Map<Order>(request);
			order.OrderKey = string.Format("{0}{1}", PrefixKeyConstant.ORDER, Guid.NewGuid().ToString().ToUpper());
			_unitOfWork.OrderRepository.Add(order);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<OrderResponse>(order);
		}

		public async Task<OrderResponse> UpdateOrder(string orderKey, UpdateOrderRequest request)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey);
			if (order == null) return null;

			_mapper.Map(request, order);
			_unitOfWork.OrderRepository.Update(order);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<OrderResponse>(order);
		}

		public async Task<bool> DeleteOrder(string orderKey)
		{
			var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderKey);
			if (order == null) return false;

			_unitOfWork.OrderRepository.Delete(order);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<OrderResponse>> GetAllOrders(GetListOrderRequest request)
		{
			var orders = await _unitOfWork.OrderRepository.GetAllAsync();
			var mapperList = _mapper.Map<IEnumerable<OrderResponse>>(orders);
			return await mapperList.ToPaginateAsync(request);
		}
	}
}
