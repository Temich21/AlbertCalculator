using AlbertCalculator.Data;
using AlbertCalculator.Models;

namespace AlbertCalculator.Repositories
{
    public class ProductsCategoriesRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public ProductsCategoriesRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<ProductsCategories> CreateConnectionAsync(ProductsCategories productsCategories)
        {
            _context.ProductsCategories.Add(productsCategories);
            await _context.SaveChangesAsync();
            return productsCategories;
        }

        public async Task DeleteConnectionAsync(ProductsCategories productsCategories)
        {
            _context.ProductsCategories.Remove(productsCategories);
            await _context.SaveChangesAsync();
        }
    }
}
