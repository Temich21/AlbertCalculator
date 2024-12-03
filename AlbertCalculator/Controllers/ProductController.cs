using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Service;
using Microsoft.AspNetCore.Mvc;

namespace AlbertCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost()]
        public async Task<ActionResult<List<Product>>> GetProducts([FromBody] GetProductDto getProductDtos)
        {
            List<Product> result = await _productService.GetProducts(getProductDtos);

            return Ok(result);
        }
    }
}
