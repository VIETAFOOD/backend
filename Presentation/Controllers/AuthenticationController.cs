using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Dto.Admin;
using Services.Interfaces;
using BusinessObjects.Dto.Coupon;
using BusinessObjects.Entities;
using Services.Extentions;
namespace Presentation.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAdminService _adminService;
        public AuthenticationController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			var response = await _adminService.Login(request);
			if(response == null)
			{
                return NotFound(new VietaFoodResponse<LoginResponse>(false, "Not found", null));
            }
            return Ok(new VietaFoodResponse<LoginResponse>(true, "Ok", response));
        }
	}
}
