using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/order")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		//private readonly IOrderService _service;

		//public OrderController(IOrderService service)
		//{
		//	_service = service;
		//}

		/// <summary>
		/// Get list order (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] PagingRequest request)
		{
			//var response = await _service.GetList(request);
			//if (response == null || response.TotalCount == 0)
			//{
			//	return NotFound();
			//}
			//return Ok(response);
			return Ok();
		}

		/// <summary>
		/// Get order by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			//var response = await _service.GetById(id);
			//if (response == null)
			//{
			//	return NotFound();
			//}
			//return Ok(response);
			return Ok();
		}

		/// <summary>
		/// Create a order
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> Create(/*CreateOrderRequest request*/)
		{
			//var response = await _service.Create(request);
			//if (response == null)
			//{
			//	return NotFound();
			//}
			//return Ok(response);
			return Ok();

		}

		/// <summary>
		/// Update a order
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateOrder(int id/*, [FromBody] UpdateOrderRequest request*/)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			//if (id != request.OrderId)
			//{
			//	return BadRequest("Order ID in the request body does not match the ID in the URL.");
			//}

			//var response = await _service.Update(request);
			//if (response == null)
			//{
			//	return NotFound();
			//}

			//return Ok(response);

			return Ok();

		}

		/// <summary>
		/// Delete a Order
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			//var result = await _service.Delete(id);

			//if (result)
			//{
			//	return Ok(true);
			//}
			//return NotFound();
			return Ok();
		}
	}
}
