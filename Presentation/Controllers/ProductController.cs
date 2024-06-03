﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Request.Paging;
using Services.Interfaces;

namespace Presentation.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductController(IProductService service)
		{
			_service = service;
		}

		/// <summary>
		/// Get list product (optional: by condition)
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpGet()]
		public async Task<IActionResult> GetList([FromQuery]PagingRequest request)
		{
			var response = await _service.GetList(request);
			if (response == null || response.TotalCount == 0)
			{
				return NotFound();
			}
			return Ok(response);
		}

		/// <summary>
		/// Get product by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var response = await _service.GetById(id);
			if (response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		/// <summary>
		/// Create a product
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost()]
		public async Task<IActionResult> Create(/*CreateProductRequest request*/)
		{
			//var response = await _service.Create(request);
			//if (response == null)
			//{
			//	return NotFound();
			//}
			//return Ok(response);
			return Ok();

		}

		/// <summary>
		/// Update a product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id/*, [FromBody] UpdateProductRequest request*/)
		{
			//if (!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);
			//}

			//if (id != request.ProductId)
			//{
			//	return BadRequest("Product ID in the request body does not match the ID in the URL.");
			//}

			//var response = await _service.Update(request);
			//if (response == null)
			//{
			//	return NotFound();
			//}

			//return Ok(response);

			return Ok();

		}

		/// <summary>
		/// Delete a Product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			//var result = await _service.Delete(id);

			//if (result)
			//{
			//	return Ok(true);
			//}
			//return NotFound();
			return Ok();
		}
	}
}
