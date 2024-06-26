using BusinessObjects.Dto.Invoices;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class InvoiceProfile : ProductProfile
    {
        public InvoiceProfile() {
            CreateMap<OrderDetail, InvoiceResponse>()
                .ForMember(dest => dest.CutomerInfo, src => src.MapFrom(x => x.OrderKeyNavigation.CustomerInfoKeyNavigation))
                .ForMember(dest => dest.Product, src => src.MapFrom(x => x.ProductKeyNavigation))
                .ReverseMap();
        }
    }
}
