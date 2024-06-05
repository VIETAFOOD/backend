using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Services.Classes
{
	public class OrderDetailService : IOrderDetailService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<OrderDetailResponse> GetById(string id)
		{
			var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id, keyColumn: "orderDetailKey");
			return orderDetail == null ? null : _mapper.Map<OrderDetailResponse>(orderDetail);
		}

		public async Task<OrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request)
		{
			var orderDetail = _mapper.Map<OrderDetail>(request);
			orderDetail.OrderDetailKey = string.Format("{0}{1}", PrefixKeyConstant.ORDER_DETAIL, Guid.NewGuid().ToString().ToUpper());
			_unitOfWork.OrderDetailRepository.Add(orderDetail);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<OrderDetailResponse>(orderDetail);
		}

		public async Task<OrderDetailResponse> UpdateOrderDetail(string id, UpdateOrderDetailRequest request)
		{
			var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id, keyColumn: "orderDetailKey");
			if (orderDetail == null) return null;

			_mapper.Map(request, orderDetail);
			_unitOfWork.OrderDetailRepository.Update(orderDetail);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<OrderDetailResponse>(orderDetail);
		}

		public async Task<bool> DeleteOrderDetail(string id)
		{
			var orderDetail = await _unitOfWork.OrderDetailRepository.GetByIdAsync(id, keyColumn: "orderDetailKey");
			if (orderDetail == null) return false;

			_unitOfWork.OrderDetailRepository.Delete(orderDetail);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<OrderDetailResponse>> GetAllOrderDetails(GetListOrderDetailRequest request)
		{
			var orderDetails = await _unitOfWork.OrderDetailRepository.GetAllAsync();
			var mapperList = _mapper.Map<IEnumerable<OrderDetailResponse>>(orderDetails);
			return await mapperList.ToPaginateAsync(request);
		}
	}
}
