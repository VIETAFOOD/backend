using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.Order;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface IOrderService
	{
		Task<OrderResponse> GetById(string orderKey);
		Task<OrderResponse> CreateOrder(CreateOrderRequest request);
		Task<OrderResponse> UpdateOrder(string orderKey, UpdateOrderRequest request);
		Task<bool> DeleteOrder(string orderKey);
		Task<PaginatedList<OrderResponse>> GetAllOrders(GetListOrderRequest request);
	}
}
