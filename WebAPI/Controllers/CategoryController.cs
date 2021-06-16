using Application.DtoModels.CategoryModels;
using Application.Service.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Manager")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
      
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
      
        [SwaggerOperation(Summary =("Retrieves all categories"))]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }
      
        [SwaggerOperation(Summary ="Retrieves one category with products ")]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryWithProducts([FromRoute] int id) 
        {
           var category= await _categoryService.GetCategoryAsync(id);
            return Ok(category);
        }
       
        [SwaggerOperation(Summary = "Create a product category")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            return Ok(await _categoryService.AddCategoryAsync(categoryDto));
        }
      
        [SwaggerOperation(Summary ="Rename the category")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return NoContent();
        }
        
        [SwaggerOperation(Summary = "Delete category")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
