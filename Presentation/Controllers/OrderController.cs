using BusinessObjects.Dto.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/order")]
	[ApiController]
    [Authorize]
    public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListOrderRequest request)
		{
			try
			{
				var orders = await _orderService.GetAllOrders(request);
				return Ok(new VietaFoodResponse<PaginatedList<OrderResponse>>(true, "Orders retrieved successfully", orders));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<IEnumerable<OrderResponse>>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpGet("{orderKey}")]
		public async Task<IActionResult> GetById(string orderKey)
		{
			try
			{
				var order = await _orderService.GetById(orderKey);
				if (order == null)
				{
					return NotFound(new VietaFoodResponse<OrderResponse>(false, "Order not found", null));
				}
				return Ok(new VietaFoodResponse<OrderResponse>(true, "Order retrieved successfully", order));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<OrderResponse>(false, "Invalid data", null));
			}

			try
			{
				var order = await _orderService.CreateOrder(request);
				return CreatedAtAction(nameof(GetById), new { orderKey = order.OrderKey }, new VietaFoodResponse<OrderResponse>(true, "Order created successfully", order));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPut("{orderKey}")]
		public async Task<IActionResult> Update(string orderKey, [FromBody] UpdateOrderRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<OrderResponse>(false, "Invalid data", null));
			}

			try
			{
				var order = await _orderService.UpdateOrder(orderKey, request);
				if (order == null)
				{
					return NotFound(new VietaFoodResponse<OrderResponse>(false, "Order not found", null));
				}
				return Ok(new VietaFoodResponse<OrderResponse>(true, "Order updated successfully", order));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpDelete("{orderKey}")]
		public async Task<IActionResult> Delete(string orderKey)
		{
			try
			{
				var result = await _orderService.DeleteOrder(orderKey);
				if (!result)
				{
					return NotFound(new VietaFoodResponse<bool>(false, "Order not found", false));
				}
				return Ok(new VietaFoodResponse<bool>(true, "Order deleted successfully", true));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<bool>(false, "An error occurred. Please try again later.", false));
			}
		}
	}
}
