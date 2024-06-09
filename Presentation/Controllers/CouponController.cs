using AutoMapper;
using BusinessObjects.Dto.Coupon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Extentions.Paginate;
using Services.Extentions;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Presentation.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponController : ControllerBase
	{
		private readonly ICouponService _couponService;
		private readonly IMapper _mapper;

		public CouponController(ICouponService couponService, IMapper mapper)
		{
			_couponService = couponService;
			_mapper = mapper;
		}

		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery] GetListCouponRequest request)
		{
			try
			{
				var coupons = await _couponService.GetAllCoupons(request);
				return Ok(new VietaFoodResponse<PaginatedList<CouponResponse>>(true, "Coupons retrieved successfully", coupons));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<PaginatedList<CouponResponse>>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpGet("{couponCode}")]
		public async Task<IActionResult> GetById(string couponCode)
		{
			try
			{
				var coupon = await _couponService.GetById(couponCode);
				if (coupon == null)
				{
					return NotFound(new VietaFoodResponse<CouponResponse>(false, "Coupon not found", null));
				}
				return Ok(new VietaFoodResponse<CouponResponse>(true, "Coupon retrieved successfully", coupon));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<CouponResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create([FromBody] CreateCouponRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<CouponResponse>(false, "Invalid data", null));
			}

			try
			{
				var coupon = await _couponService.CreateCoupon(request);
				if(coupon == null)
				{
					return BadRequest(coupon);
				}
				return Ok(coupon);
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<CouponResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpPut("{key}")]
        [Authorize]
        public async Task<IActionResult> Update(string key, [FromBody] UpdateCouponRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<CouponResponse>(false, "Invalid data", null));
			}

			try
			{
				var coupon = await _couponService.UpdateCoupon(key, request);
				if (coupon == null)
				{
					return NotFound(new VietaFoodResponse<CouponResponse>(false, "Coupon not found", null));
				}
				return Ok(new VietaFoodResponse<CouponResponse>(true, "Coupon updated successfully", coupon));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<CouponResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		[HttpDelete("{key}")]
        [Authorize]
        public async Task<IActionResult> Delete(string key)
		{
			try
			{
				var result = await _couponService.DeleteCoupon(key);
				if (!result)
				{
					return NotFound(new VietaFoodResponse<bool>(false, "Coupon not found", false));
				}
				return Ok(new VietaFoodResponse<bool>(true, "Coupon deleted successfully", true));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<bool>(false, "An error occurred. Please try again later.", false));
			}
		}
	}
}
