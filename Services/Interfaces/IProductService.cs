using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.Product;
using BusinessObjects.Entities;
using Services.Extentions.Paginate;

namespace Services.Interfaces
{
	public interface IProductService
	{
		Task<ProductResponse> Create(CreateProductRequest request);
		Task<ProductResponse> Update(UpdateProductRequest request);
		Task<ProductResponse> GetById(int id);
		Task<PaginatedList<ProductResponse>> GetList();
		Task<bool> Delete(int id);
	}
}
