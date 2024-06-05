using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Entities;
using Repositories;
using Services.Constant;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Services.Classes
{
	public class CustomerInformationService : ICustomerInformationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerInformationService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerInformationResponse> GetById(string id)
		{
			var customerInformation = await _unitOfWork.CustomerInformationRepository.GetByIdAsync(id, keyColumn: "customerInfoKey");
			return customerInformation == null ? null : _mapper.Map<CustomerInformationResponse>(customerInformation);
		}

		public async Task<CustomerInformationResponse> CreateCustomerInformation(CreateCustomerInformationRequest request)
		{
			var customerInformation = _mapper.Map<CustomerInformation>(request);
			customerInformation.CustomerInfoKey = string.Format("{0}{1}", PrefixKeyConstant.CUSTOMER_INFO, Guid.NewGuid().ToString().ToUpper());
			_unitOfWork.CustomerInformationRepository.Add(customerInformation);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<CustomerInformationResponse>(customerInformation);
		}

		public async Task<CustomerInformationResponse> UpdateCustomerInformation(string id, UpdateCustomerInformationRequest request)
		{
			var customerInformation = await _unitOfWork.CustomerInformationRepository.GetByIdAsync(id, keyColumn: "customerInfoKey");
			if (customerInformation == null) return null;

			_mapper.Map(request, customerInformation);
			_unitOfWork.CustomerInformationRepository.Update(customerInformation);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerInformationResponse>(customerInformation);
		}

		public async Task<bool> DeleteCustomerInformation(string id)
		{
			var customerInformation = await _unitOfWork.CustomerInformationRepository.GetByIdAsync(id, keyColumn: "customerInfoKey");
			if (customerInformation == null) return false;

			_unitOfWork.CustomerInformationRepository.Delete(customerInformation);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<CustomerInformationResponse>> GetAllCustomerInformations(GetListCustomerInformationRequest request)
		{
			var customerInformations = await _unitOfWork.CustomerInformationRepository.GetAllAsync();
			var mapperList = _mapper.Map<IEnumerable<CustomerInformationResponse>>(customerInformations);
			return await mapperList.ToPaginateAsync(request);
		}
	}
}
