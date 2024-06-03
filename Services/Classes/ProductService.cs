using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Product;
using Repositories;
using Request.Paging;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Services.Classes
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public Task<ProductResponse> Create(CreateProductRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<ProductResponse> GetById(string id)
		{
			var result = _unitOfWork.ProductRepository.GetByID(id);
			if(result == null)
			{
				return null;
			}
			var response = _mapper.Map<ProductResponse>(result);
			return response;
		}

		public async Task<PaginatedList<ProductResponse>> GetList(PagingRequest request)
		{
			var list = _unitOfWork.ProductRepository.GetAll();
			var listResponse = _mapper.Map<IList<ProductResponse>>(list);
			return await listResponse.ToPaginateAsync(request);
		}

		public Task<ProductResponse> Update(UpdateProductRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
