using AlbertCalculator.Data;
using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbertCalculator.Repositories
{
    public class ProductRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public ProductRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> FindAllByNameAsync(GetProductDto getProductDto)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(getProductDto.Name))
                .ToListAsync();
        }
    }
}
