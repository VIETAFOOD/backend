using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Coupon;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
using Services.Extentions.Paginate;
using Services.Interfaces;


namespace Services.Classes
{
	public class CouponService : ICouponService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CouponResponse> GetById(string couponKey)
		{
			var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponKey);
			return coupon == null ? null : _mapper.Map<CouponResponse>(coupon);
		}

		public async Task<CouponResponse> CreateCoupon(CreateCouponRequest request)
		{
			var coupon = _mapper.Map<Coupon>(request);
			coupon.CouponKey = string.Format("{0}{1}", PrefixKeyConstant.COUPON, Guid.NewGuid().ToString().ToUpper());
			_unitOfWork.CouponRepository.Add(coupon);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<CouponResponse>(coupon);
		}

		public async Task<CouponResponse> UpdateCoupon(string couponKey, UpdateCouponRequest request)
		{
			var existingCoupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponKey);
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
			var existingCoupon = await _unitOfWork.CouponRepository.GetByIdAsync(couponKey);
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
			var coupons = await _unitOfWork.CouponRepository.GetAllAsync();
			var mappedCoupons = _mapper.Map<IEnumerable<CouponResponse>>(coupons);
			return await mappedCoupons.ToPaginateAsync(request);
		}
	}
}