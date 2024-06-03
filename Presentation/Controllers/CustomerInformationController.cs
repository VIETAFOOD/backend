using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;

namespace Presentation.Controllers
{
	[Route("api/customer-information")]
	[ApiController]
	public class CustomerInformationController : ControllerBase
	{
		//private readonly ICustomerInformationService _service;

		//public CustomerInformationController(ICustomerInformationService service)
		//{
		//	_service = service;
		//}

		/// <summary>
		/// Get list customerInformation (optional: by condition)
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
		/// Get customerInformation by id
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
		/// Create a customerInformation
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> Create(/*CreateCustomerInformationRequest request*/)
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
		/// Update a customerInformation
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomerInformation(int id/*, [FromBody] UpdateCustomerInformationRequest request*/)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			//if (id != request.CustomerInformationId)
			//{
			//	return BadRequest("CustomerInformation ID in the request body does not match the ID in the URL.");
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
		/// Delete a CustomerInformation
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
