using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.OrderDetail;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class CustomerInfoProfile : Profile
	{
        public CustomerInfoProfile()
        {
			CreateMap<CreateCustomerInformationRequest, CustomerInformation>();
			CreateMap<UpdateCustomerInformationRequest, CustomerInformation>();
			CreateMap<CustomerInformation, CustomerInformationResponse>();
		}
    }
}
