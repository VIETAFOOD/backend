using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.CustomerInformation;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface ICustomerInformationService
	{
		Task<CustomerInformationResponse> GetById(string id);
		Task<CustomerInformationResponse> CreateCustomerInformation(CreateCustomerInformationRequest request);
		Task<CustomerInformationResponse> UpdateCustomerInformation(string id, UpdateCustomerInformationRequest request);
		Task<bool> DeleteCustomerInformation(string id);
		Task<PaginatedList<CustomerInformationResponse>> GetAllCustomerInformations(GetListCustomerInformationRequest request);
	}
}
