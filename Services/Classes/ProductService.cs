using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;
using Repositories;
using Request.Paging;
using Services.Constant;
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

		public async Task<ProductResponse> GetById(string id)
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			return product == null ? null : _mapper.Map<ProductResponse>(product);
		}

		public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
		{
			var product = _mapper.Map<Product>(request);
			product.ProductKey = string.Format("{0}{1}", PrefixKeyConstant.PRODUCT, Guid.NewGuid().ToString().ToUpper());
			_unitOfWork.ProductRepository.Add(product);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<ProductResponse>(product);
		}

		public async Task<ProductResponse> UpdateProduct(string id, UpdateProductRequest request)
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null) return null;

			_mapper.Map(request, product);
			_unitOfWork.ProductRepository.Update(product);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<ProductResponse>(product);
		}

		public async Task<bool> DeleteProduct(string id)
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null) return false;

			_unitOfWork.ProductRepository.Delete(product);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<ProductResponse>> GetAllProducts(GetListProductRequest request)
		{
			var products = await _unitOfWork.ProductRepository.GetAllAsync();
			var mapperList = _mapper.Map<IEnumerable<ProductResponse>>(products);
			return await mapperList.ToPaginateAsync(request);
		}
	}

}
