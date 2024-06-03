using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;
using Request.Paging;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface IProductService
	{
		Task<ProductResponse> GetById(string id);
		Task<ProductResponse> CreateProduct(CreateProductRequest request);
		Task<ProductResponse> UpdateProduct(string id, UpdateProductRequest request);
		Task<bool> DeleteProduct(string id);
		Task<PaginatedList<ProductResponse>> GetAllProducts(GetListProductRequest request);
	}
}
