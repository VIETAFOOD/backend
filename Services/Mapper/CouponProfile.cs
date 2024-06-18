using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Coupon;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class CouponProfile : Profile
	{
        public CouponProfile()
        {
			CreateMap<CreateCouponRequest, Coupon>();
			CreateMap<UpdateCouponRequest, Coupon>();
			CreateMap<Coupon, CouponResponse>()
				.ForMember(dest => dest.Email, src => src.MapFrom(x => x.CreatedByNavigation.Email));
		}
    }
}
