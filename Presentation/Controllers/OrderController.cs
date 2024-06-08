using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;
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
    public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[Authorize]
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

		[Authorize]
		[HttpGet("{key}")]
		public async Task<IActionResult> GetById(string key)
		{
			try
			{
				var order = await _orderService.GetById(key);
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

		[ProducesResponseType(typeof(VietaFoodResponse<OrderResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status500InternalServerError)]
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
				if(order == null)
				{
                    return BadRequest(new VietaFoodResponse<OrderResponse>(false, "Bad Request", order));
                }
                return Ok(new VietaFoodResponse<OrderResponse>(true, "Order created successfully", order));
            }
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPut("{key}")]
		[Authorize]
		public async Task<IActionResult> Update(string key, [FromBody] UpdateOrderRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<OrderResponse>(false, "Invalid data", null));
			}

			try
			{
				var order = await _orderService.UpdateOrder(key, request);
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

		[HttpDelete("{key}")]
		public async Task<IActionResult> Delete(string key)
		{
			try
			{
				var result = await _orderService.DeleteOrder(key);
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
