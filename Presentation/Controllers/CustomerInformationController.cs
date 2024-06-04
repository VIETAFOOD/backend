using BusinessObjects.Dto.CustomerInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/customer")]
	[ApiController]
    [Authorize]
    public class CustomerInformationController : ControllerBase
	{
		private readonly ICustomerInformationService _customerInformationService;

		public CustomerInformationController(ICustomerInformationService customerInformationService)
		{
			_customerInformationService = customerInformationService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList([FromQuery] GetListCustomerInformationRequest request)
		{
			try
			{
				var customerInformations = await _customerInformationService.GetAllCustomerInformations(request);
				return Ok(new VietaFoodResponse<PaginatedList<CustomerInformationResponse>>(true, "Customer informations retrieved successfully", customerInformations));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<PaginatedList<CustomerInformationResponse>>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpGet("{key}")]
		public async Task<IActionResult> GetById(string key)
		{
			try
			{
				var customerInformation = await _customerInformationService.GetById(key);
				if (customerInformation == null)
				{
					return NotFound(new VietaFoodResponse<CustomerInformationResponse>(false, "Customer information not found", null));
				}
				return Ok(new VietaFoodResponse<CustomerInformationResponse>(true, "Customer information retrieved successfully", customerInformation));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<CustomerInformationResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCustomerInformationRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<CustomerInformationResponse>(false, "Invalid data", null));
			}

			try
			{
				var customerInformation = await _customerInformationService.CreateCustomerInformation(request);
				return CreatedAtAction(nameof(GetById), new { customerInfoKey = customerInformation.CustomerInfoKey }, new VietaFoodResponse<CustomerInformationResponse>(true, "Customer information created successfully", customerInformation));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<CustomerInformationResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPut("{key}")]
		public async Task<IActionResult> Update(string key, [FromBody] UpdateCustomerInformationRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<CustomerInformationResponse>(false, "Invalid data", null));
			}

			try
			{
				var customerInformation = await _customerInformationService.UpdateCustomerInformation(key, request);
				if (customerInformation == null)
				{
					return NotFound(new VietaFoodResponse<CustomerInformationResponse>(false, "Customer information not found", null));
				}
				return Ok(new VietaFoodResponse<CustomerInformationResponse>(true, "Customer information updated successfully", customerInformation));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<CustomerInformationResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpDelete("{key}")]
		public async Task<IActionResult> Delete(string key)
		{
			try
			{
				var result = await _customerInformationService.DeleteCustomerInformation(key);
				if (!result)
				{
					return NotFound(new VietaFoodResponse<bool>(false, "Customer information not found", false));
				}
				return Ok(new VietaFoodResponse<bool>(true, "Customer information deleted successfully", true));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new VietaFoodResponse<bool>(false, "An error occurred. Please try again later.", false));
			}
		}
	}
}
