using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Repositories;
namespace AlbertCalculator.Service
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProducts(GetProductDto getProductDtos)
        {
            return await _productRepository.FindAllByNameAsync(getProductDtos);
        }

    }
}
