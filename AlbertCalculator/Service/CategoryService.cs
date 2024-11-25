using AlbertCalculator.Dtos;
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

        //public async Task<ProductsCategoriesDto> CreateConnection(ProductsCategoriesDto productsCategoriesDto)
        //{
        //    return await _categoryRepository.CreateConnection(productsCategoriesDto);
        //}

        //public async Task<Guid> DeleteConnection(ProductsCategoriesDto productsCategoriesDto)
        //{
        //    return await _categoryRepository.DeleteConnectionAsync(productsCategoriesDto);
        //}
    }
}
