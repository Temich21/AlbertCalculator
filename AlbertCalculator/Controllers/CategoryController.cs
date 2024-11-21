using Microsoft.AspNetCore.Mvc;
using AlbertCalculator.Dtos;
using AlbertCalculator.Service;
using System.Threading.Tasks;

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
    }
}
