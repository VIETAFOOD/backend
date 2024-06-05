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
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, keyColumn: "productKey");
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
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, keyColumn: "productKey");
			if (product == null) return null;

			_mapper.Map(request, product);
			_unitOfWork.ProductRepository.Update(product);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<ProductResponse>(product);
		}

		public async Task<bool> DeleteProduct(string id)
		{
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, keyColumn: "productKey");
			if (product == null) return false;

			_unitOfWork.ProductRepository.Delete(product);
			await _unitOfWork.CommitAsync();

			return true;
		}

		public async Task<PaginatedList<ProductResponse>> GetAllProducts(GetListProductRequest request)
		{
			// Get the base queryable from the repository
			var query = await _unitOfWork.ProductRepository.GetAllAsync();

			// Apply filtering
			if (!string.IsNullOrEmpty(request.Name))
			{
				var nameFilter = request.Name.ToLower();
				query = query.Where(x => x.Name.ToLower().Contains(nameFilter));
			}

			// Apply sorting
			if (!string.IsNullOrEmpty(request.SortOption))
			{
				switch (request.SortOption.ToLower())
				{
					case "name":
						query = request.isSortDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
						break;
					case "price":
						query = request.isSortDesc ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price);
						break;
				}
			}

			// Project to the response type and paginate
			var mappedQuery = query.Select(x => _mapper.Map<ProductResponse>(x));
			return await mappedQuery.ToPaginateAsync(request);
		}

	}

}
