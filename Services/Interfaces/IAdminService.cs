using BusinessObjects.Dto.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IAdminService
	{
		Task<LoginResponse> Login(LoginRequest request);
	}
}
