using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Admin;
using Repositories;
using Services.Extentions;
using Services.Interfaces;

namespace Services.Classes
{
    public class AdminService : IAdminService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var getData = _unitOfWork.AdminRepository
                            .Get(filter: x => x.Email.Equals(request.email) 
                                    && x.Password.Equals(request.password))
                            .FirstOrDefault();
            if(getData == null)
            {
                return null;
            }

            var response =  _mapper.Map<LoginResponse>(getData);
            response.token = Utils.GenerteDefaultToken(getData);

            return response;
        }
    }
}
