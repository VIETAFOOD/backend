using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<CreateOrderRequest, Order>();
			CreateMap<UpdateOrderRequest, Order>();
			CreateMap<Order, OrderResponse>();
		}
	}
}
