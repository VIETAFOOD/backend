using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Admin;
using BusinessObjects.Entities;

namespace Services.Mapper
{
	public class AdminProfile : Profile
	{
        public AdminProfile()
        {
            CreateMap<LoginRequest, Admin>().ReverseMap();
            CreateMap<Admin, LoginResponse>()
                .ForMember(dst => dst.email, src => src.MapFrom(x => x.Email));
        }
    }
}
