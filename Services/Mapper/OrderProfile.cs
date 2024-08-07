﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Dto.Product;
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
			CreateMap<UpdateOrderRequest, Order>()
				.ForMember(dest => dest.ImgUrl, opt => opt.Condition(src => src.ImgUrl != null));
			CreateMap<Order, OrderResponse>()	
			.ForMember(dest => dest.CustomerInfo, opt => opt.MapFrom(src => src.CustomerInfoKeyNavigation))
			.ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
			.ForMember(dest => dest.Status, opt => opt.MapFrom(src => Utils.GetDescriptionEnum((OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), src.Status.ToString()))));

			CreateMap<CustomerInformation, CustomerInformationResponse>();

			CreateMap<OrderDetail, OrderDetailResponse>()
				.ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.ProductKeyNavigation));

			CreateMap<Product, ProductResponse>();
		}
	}
}
