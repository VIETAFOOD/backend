using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class OrderDetailProfile : Profile
	{
        public OrderDetailProfile()
        {
			CreateMap<CreateOrderDetailRequest, OrderDetail>();
			CreateMap<UpdateOrderDetailRequest, OrderDetail>();
			CreateMap<OrderDetail, OrderDetailResponse>()
				.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.ProductKeyNavigation));
		}
	}
}
