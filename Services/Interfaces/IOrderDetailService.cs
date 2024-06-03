using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.OrderDetail;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface IOrderDetailService
	{
		Task<OrderDetailResponse> GetById(string id);
		Task<OrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request);
		Task<OrderDetailResponse> UpdateOrderDetail(string id, UpdateOrderDetailRequest request);
		Task<bool> DeleteOrderDetail(string id);
		Task<PaginatedList<OrderDetailResponse>> GetAllOrderDetails(GetListOrderDetailRequest request);
	}
}
