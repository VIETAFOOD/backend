using AutoMapper;
using BusinessObjects.Dto.Coupon;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Constant;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;


namespace Services.Classes
{
	public class CouponService : ICouponService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _contextAccessor;

		public CouponService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_contextAccessor = contextAccessor;
		}

		public async Task<CouponResponse> GetById(string couponCode)
		{
			var coupon = _unitOfWork.CouponRepository
										.Get(filter: x =>
											x.CouponCode.Equals(couponCode)
											&& x.ExpiredDate > Utils.GetDateTimeNow()
											&& x.NumOfUses >= 1)
										.FirstOrDefault();
			return coupon == null ? null : _mapper.Map<CouponResponse>(coupon);
		}

		public async Task<CouponResponse> CreateCoupon(CreateCouponRequest request)
		{
			var getKeyAdmin = _contextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

			var exsitAdmin = _unitOfWork.AdminRepository
											.Get(filter: x => x.AdminKey.Equals(getKeyAdmin)
															&& x.Email.Equals(request.Email))
											.FirstOrDefault();

			var coupon = _mapper.Map<Coupon>(request);
            coupon.CreatedDate = Utils.GetDateTimeNow();

            coupon.ExpiredDate = coupon.CreatedDate.AddMonths(request.ExpiredDate);
            if (coupon.CreatedDate > coupon.ExpiredDate
					|| coupon.NumOfUses < 0
					|| (request.DiscountPercentage > 100
					|| request.DiscountPercentage < 0)
					|| exsitAdmin == null
				)
            {
                return null;
            }
            
            coupon.Status = PrefixKeyConstant.TRUE;
            coupon.CouponKey = string.Format("{0}{1}", PrefixKeyConstant.COUPON, Guid.NewGuid().ToString().ToUpper());
			coupon.CreatedBy = exsitAdmin.AdminKey;
            _unitOfWork.CouponRepository.Add(coupon);
			_unitOfWork.Commit();

			var response = _mapper.Map<CouponResponse>(coupon);
			response.Email = exsitAdmin.Email;
			return response;

        }

		public async Task<CouponResponse> UpdateCoupon(string couponKey, UpdateCouponRequest request)
		{
			var existingCoupon = _unitOfWork.CouponRepository
												.Get(filter: x => x.CouponKey.Equals(couponKey))
												.FirstOrDefault();
			if (existingCoupon == null)
			{
				return null;
			}

			_mapper.Map(request, existingCoupon);
			_unitOfWork.CouponRepository.Update(existingCoupon);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CouponResponse>(existingCoupon);
		}

		public async Task<bool> DeleteCoupon(string couponKey)
		{
			var existingCoupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponKey, keyColumn: "couponKey");
			if (existingCoupon == null)
			{
				return false;
			}

			_unitOfWork.CouponRepository.Delete(existingCoupon);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<CouponResponse>> GetAllCoupons(GetListCouponRequest request)
		{
            IEnumerable<Coupon> coupons;
            if (request.CouponCode == null)
            {
                coupons = request.DiscountPercentage == null
                    ? await _unitOfWork.CouponRepository.GetAllAsync()
                    : _unitOfWork.CouponRepository.Get(filter: x => x.DiscountPercentage == request.DiscountPercentage);
            }
            else
            {
                coupons = _unitOfWork.CouponRepository.Get(filter: x => x.CouponCode.Equals(request.CouponCode)
                                                        && (request.DiscountPercentage == null 
														|| x.DiscountPercentage == request.DiscountPercentage));
            }

            var response = _mapper.Map<IEnumerable<CouponResponse>>(coupons);
			return await response.ToPaginateAsync(request);
		}
	}
}