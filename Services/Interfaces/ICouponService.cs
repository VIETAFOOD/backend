using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.Coupon;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface ICouponService
	{
		Task<CouponResponse> GetById(string couponKey);
		Task<PaginatedList<CouponResponse>> GetAllCoupons(GetListCouponRequest request);
		Task<CouponResponse> CreateCoupon(CreateCouponRequest request);
		Task<CouponResponse> UpdateCoupon(string couponKey, UpdateCouponRequest request);
		Task<bool> DeleteCoupon(string couponKey);
	}
}
