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
		[ProducesResponseType(typeof(VietaFoodResponse<PaginatedList<ProductResponse>>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery]GetListProductRequest request)
		{
			try
			{
				var products = await _productService.GetAllProducts(request);
				if(products.TotalCount <= 0)
				{
					return StatusCode(404, new VietaFoodResponse<PaginatedList<ProductResponse>>(false, "Products not found", null));
				}
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
		[ProducesResponseType(typeof(VietaFoodResponse<ProductResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status500InternalServerError)]
		[HttpGet("{key}")]
		public async Task<IActionResult> GetById(string key)
		{
			try
			{
				var product = await _productService.GetById(key);
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
		[ProducesResponseType(typeof(VietaFoodResponse<ProductResponse>), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status400BadRequest)]
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
				return  Ok(product);
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
		/// <param name="key"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(VietaFoodResponse<ProductResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status404NotFound)]
		[HttpPut("{key}")]
        [Authorize]
        public async Task<IActionResult> Update(string key, [FromBody] UpdateProductRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new VietaFoodResponse<ProductResponse>(false, "Invalid data", null));
			}

			try
			{
				var product = await _productService.UpdateProduct(key, request);
				if (product == null)
				{
					return NotFound(new VietaFoodResponse<ProductResponse>(false, "Product not found", null));
				}
				return Ok(product);
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
		/// <param name="key"></param>
		/// <returns></returns>
		[ProducesResponseType(typeof(VietaFoodResponse<bool>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(VietaFoodResponse<>), StatusCodes.Status404NotFound)]
		[HttpDelete("{key}")]
        [Authorize]
        public async Task<IActionResult> Delete(string key)
		{
			try
			{
				var result = await _productService.DeleteProduct(key);
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
