using Application.DtoModels.ProductModels;
using Application.Filters;
using Application.Helpers;
using Application.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Authorize(Roles ="Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [SwaggerOperation(Summary = "Receiving all products")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts([FromQuery]ProductParameters productParameters)
        {
            var result = await _productService.GetAllProducts(productParameters);
            var totalRecords = await _productService.GetAllProductsCountAsync();
            return Ok(PaginationHelper.CreatePagedResponse(result,productParameters, totalRecords));
        }

        [SwaggerOperation(Summary = "create product for a category")]
        [HttpPost("{categoryId}")]
        public async Task<IActionResult> CreateProduct([FromRoute]int categoryId,[FromBody]CreateProductDto productDto)
        {
            await _productService.AddProductAsync(categoryId, productDto);
            return Ok();
        }

        [SwaggerOperation(Summary = "Update product details")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int id,[FromBody]UpdateProductDto productDto)
        {
            await _productService.UpdateProductAsync(id, productDto);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete product")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
