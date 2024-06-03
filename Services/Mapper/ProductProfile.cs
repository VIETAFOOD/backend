using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class ProductProfile : Profile
	{
		public ProductProfile() {
			CreateMap<Product, ProductResponse>();
		}
	}
}
