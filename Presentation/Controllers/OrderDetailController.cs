using BusinessObjects.Dto.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/order-detail")]
	[ApiController]
	public class OrderDetailController : ControllerBase
	{
		private readonly IOrderDetailService _orderDetailService;

		public OrderDetailController(IOrderDetailService orderDetailService)
		{
			_orderDetailService = orderDetailService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListOrderDetailRequest request)
		{
			try
			{
				var orderDetails = await _orderDetailService.GetAllOrderDetails(request);
				return Ok(new VietaFoodResponse<PaginatedList<OrderDetailResponse>>(true, "Order details retrieved successfully", orderDetails));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<IEnumerable<OrderDetailResponse>>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpGet("{key}")]
		public async Task<IActionResult> GetById(string orderDetailKey)
		{
			try
			{
				var orderDetail = await _orderDetailService.GetById(orderDetailKey);
				if (orderDetail == null)
				{
					return NotFound(new VietaFoodResponse<OrderDetailResponse>(false, "Order detail not found", null));
				}
				return Ok(new VietaFoodResponse<OrderDetailResponse>(true, "Order detail retrieved successfully", orderDetail));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderDetailResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateOrderDetailRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<OrderDetailResponse>(false, "Invalid data", null));
			}

			try
			{
				var orderDetail = await _orderDetailService.CreateOrderDetail(request);
				return CreatedAtAction(nameof(GetById), new { orderDetailKey = orderDetail.OrderDetailKey }, new VietaFoodResponse<OrderDetailResponse>(true, "Order detail created successfully", orderDetail));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderDetailResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPut("{key}")]
		public async Task<IActionResult> Update(string orderDetailKey, [FromBody] UpdateOrderDetailRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<OrderDetailResponse>(false, "Invalid data", null));
			}

			try
			{
				var orderDetail = await _orderDetailService.UpdateOrderDetail(orderDetailKey, request);
				if (orderDetail == null)
				{
					return NotFound(new VietaFoodResponse<OrderDetailResponse>(false, "Order detail not found", null));
				}
				return Ok(new VietaFoodResponse<OrderDetailResponse>(true, "Order detail updated successfully", orderDetail));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<OrderDetailResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpDelete("{key}")]
		public async Task<IActionResult> Delete(string orderDetailKey)
		{
			try
			{
				var result = await _orderDetailService.DeleteOrderDetail(orderDetailKey);
				if (!result)
				{
					return NotFound(new VietaFoodResponse<bool>(false, "Order detail not found", false));
				}
				return Ok(new VietaFoodResponse<bool>(true, "Order detail deleted successfully", true));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<bool>(false, "An error occurred. Please try again later.", false));
			}
		}
	}
}
