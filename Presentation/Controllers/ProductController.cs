using BusinessObjects.Dto.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Classes;
using Services.Extentions;
using Services.Extentions.Paginate;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService service)
		{
			_productService = service;
		}

		/// <summary>
		/// Get list product (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery]GetListProductRequest request)
		{
			try
			{
				var products = await _productService.GetAllProducts(request);
				return Ok(new VietaFoodResponse<PaginatedList<ProductResponse>>(true, "Products retrieved successfully", products));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<PaginatedList<ProductResponse>>(false, "An error occurred. Please try again later.", null));
			}
		}

		/// <summary>
		/// Get product by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			try
			{
				var product = await _productService.GetById(id);
				if (product == null)
				{
					return NotFound(new VietaFoodResponse<ProductResponse>(false, "Product not found", null));
				}
				return Ok(new VietaFoodResponse<ProductResponse>(true, "Product retrieved successfully", product));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				// _logger.LogError(ex, "An error occurred while fetching the product");

				// Return a generic error response
				return StatusCode(500, new VietaFoodResponse<ProductResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		/// <summary>
		/// Create a product
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<ProductResponse>(false, "Invalid data", null));
			}

			try
			{
				var product = await _productService.CreateProduct(request);
				return CreatedAtAction(nameof(GetById), new { id = product.ProductKey }, new VietaFoodResponse<ProductResponse>(true, "Product created successfully", product));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<ProductResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		/// <summary>
		/// Update a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateProductRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<ProductResponse>(false, "Invalid data", null));
			}

			try
			{
				var product = await _productService.UpdateProduct(id, request);
				if (product == null)
				{
					return NotFound(new VietaFoodResponse<ProductResponse>(false, "Product not found", null));
				}
				return Ok(new VietaFoodResponse<ProductResponse>(true, "Product updated successfully", product));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<ProductResponse>(false, "An error occurred. Please try again later.", null));
			}
		}

		/// <summary>
		/// Delete a Product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
		{
			try
			{
				var result = await _productService.DeleteProduct(id);
				if (!result)
				{
					return NotFound(new VietaFoodResponse<bool>(false, "Product not found", false));
				}
				return Ok(new VietaFoodResponse<bool>(true, "Product deleted successfully", true));
			}
			catch (Exception ex)
			{
				// Log the exception (optional)
				return StatusCode(500, new VietaFoodResponse<bool>(false, "An error occurred. Please try again later.", false));
			}
		}
	}
}
