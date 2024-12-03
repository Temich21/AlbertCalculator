using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Repositories;
namespace AlbertCalculator.Service
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly ProductsCategoriesRepository _productsCategoriesRepository;

        public CategoryService(CategoryRepository categoryRepository, ProductRepository productRepository, ProductsCategoriesRepository productsCategoriesRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productsCategoriesRepository = productsCategoriesRepository;
        }

        public async Task<List<CategoryDto>> FindAll(Guid userId)
        {
            return await _categoryRepository.FindAllAsync(userId);
        }

        public async Task<List<ProductDto>> FindOne(Guid categoryId, Guid userId)
        {
            return await _categoryRepository.FindOneAsync(categoryId, userId);
        }

        public async Task<CategoryDto> CreateCategory(string name, Guid userId)
        {
            return await _categoryRepository.CreateCategoryAsync(name, userId);
        }

        public async Task<CategoryDto> UpdateCategory(CategoryDto categoryDto)
        {
            return await _categoryRepository.UpdateCategoryAsync(categoryDto);
        }

        public async Task<Guid> DeleteCategory(Guid categoryId)
        {
            return await _categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<ProductsCategories> CreateConnection(ProductsCategories productsCategories)
        {
            Category? category = await _categoryRepository.FindOneByIdAsync(productsCategories.CategoryId) 
                ?? throw new KeyNotFoundException($"Category with ID {productsCategories.CategoryId} doesn't exist.");

            Product? product = await _productRepository.FindOneByIdAsync(productsCategories.ProductId)
                ?? throw new KeyNotFoundException($"Product with ID {productsCategories.ProductId} doesn't exist.");

            return await _productsCategoriesRepository.CreateConnectionAsync(productsCategories);
        }

        public async Task DeleteConnection(ProductsCategories productsCategories)
        {
            Category? category = await _categoryRepository.FindOneByIdAsync(productsCategories.CategoryId)
                ?? throw new KeyNotFoundException($"Category with ID {productsCategories.CategoryId} doesn't exist.");

            Product? product = await _productRepository.FindOneByIdAsync(productsCategories.ProductId)
                ?? throw new KeyNotFoundException($"Product with ID {productsCategories.ProductId} doesn't exist.");

            await _productsCategoriesRepository.DeleteConnectionAsync(productsCategories);
        }
    }
}
