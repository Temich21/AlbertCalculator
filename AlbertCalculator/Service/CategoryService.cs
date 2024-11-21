using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Repositories;
namespace AlbertCalculator.Service
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> FindAll(Guid userId)
        {
            return await _categoryRepository.FindAllAsync(userId);
        }

        public async Task<List<ProductDto>> FindOne(Guid categoryId, Guid userId)
        {
            return await _categoryRepository.FindOneAsync(categoryId, userId);
        }

    }
}
