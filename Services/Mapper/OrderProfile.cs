using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;
using Services.Enums;
using Services.Extentions;

namespace Services.Mapper
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CreateOrderRequest, Order>();
			CreateMap<UpdateOrderRequest, Order>();
			CreateMap<Order, OrderResponse>()
			.ForMember(dest => dest.CustomerInfo, opt => opt.MapFrom(src => src.CustomerInfoKeyNavigation))
			.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderDetails))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => Utils.GetDescriptionEnum((OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), src.Status.ToString()))));
		}
	}
}
