using Microsoft.AspNetCore.Mvc;
using AlbertCalculator.Dtos;
using AlbertCalculator.Service;
using AlbertCalculator.Models;

namespace AlbertCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all/{userId}")]
        public async Task<ActionResult<List<CategoryDto>>> FindAllCategory(Guid userId)
        {
            var result = await _categoryService.FindAll(userId);
            
            return Ok(result);
        }

        [HttpGet("{categoryId}/{userId}")]
        public async Task<ActionResult<List<ProductDto>>> FindOneCategory(Guid categoryId, Guid userId)
        {
            var result = await _categoryService.FindOne(categoryId, userId);

            return Ok(result);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] string name, Guid userId)
        {
            var result = await _categoryService.CreateCategory(name, userId);

            return Ok(result);
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            await _categoryService.UpdateCategory(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory([FromBody] Guid categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);

            return Ok();
        }

        //[HttpPost("connection")]
        //public async Task<ActionResult<ProductsCategoriesDto>> CreateConnection([FromBody] ProductsCategoriesDto productsCategoriesDto)
        //{
        //    var result = await _categoryService.CreateConnection(productsCategoriesDto);

        //    return Ok(result);
        //}

        //[HttpDelete("connection")]
        //public async Task<ActionResult<CategoryDto>> DeleteConnection([FromBody] ProductsCategoriesDto productsCategoriesDto)
        //{
        //    await _categoryService.DeleteConnection(productsCategoriesDto);

        //    return Ok();
        //}
    }
}
