using AlbertCalculator.Data;
using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbertCalculator.Repositories
{
    public class CategoryRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public CategoryRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto> CreateCategoryAsync(string name, Guid userId)
        {
            var customCategory = new CustomCategory{
                Name = name,
                UserId = userId,
            };

            _context.Categories.Add(customCategory);
            await _context.SaveChangesAsync();

            return new CategoryDto {
                Id = customCategory.Id,
                Name = name,
                Expenses = 0
            };
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryDto.Id);

            if (category == null)
            {
                throw new Exception("Category doesn't exist.");
            }

            category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();

            return categoryDto;
        }

        public async Task<Guid> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new Exception("Category doesn't exist.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return categoryId;
        }

        public async Task<List<CategoryDto>> FindAllAsync(Guid userId)
        {
            return await _context.Categories.Where(c => c.UserId == userId || c.UserId == null)
                .Join(
                    _context.ProductsCategories,
                    c => c.Id,
                    pc => pc.CategoryId,
                    (c, pc) => new { Category = c, ProductId = pc.ProductId }
                )
                .Join(
                    _context.Products,
                    cpc => cpc.ProductId,
                    p => p.Id,
                    (cpc, p) => new { cpc.Category, Product = p }
                )
                .Join(
                    _context.PurchaseProducts,
                    cp => cp.Product.Id,
                    pp => pp.ProductId,
                    (cp, pp) => new { cp.Category, cp.Product, PurchaseProduct = pp }
                )
                .Join(
                    _context.Purchases,
                    cpp => cpp.PurchaseProduct.PurchaseId,
                    pu => pu.Id,
                    (cpp, pu) => new { cpp.Category, cpp.Product, cpp.PurchaseProduct, Purchase = pu }
                )
                .Where(x => x.Purchase.UserId == userId)
                .GroupBy(x => new { x.Category.Id, x.Category.Name })
                .Select(g => new CategoryDto
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Expenses = g.Sum(x => x.PurchaseProduct.Quantity * x.Product.Price)
                })
                .ToListAsync();
        }

        public async Task<List<ProductDto>> FindOneAsync(Guid categoryId, Guid userId)
        {
            return await _context.Categories.Where(c => c.Id == categoryId)
                .Join(
                    _context.ProductsCategories,
                    c => c.Id,
                    pc => pc.CategoryId,
                    (c, pc) => new { Category = c, ProductId = pc.ProductId }
                )
                .Join(
                    _context.Products,
                    cpc => cpc.ProductId,
                    p => p.Id,
                    (cpc, p) => new { cpc.Category, Product = p }
                )
                .Join(
                    _context.PurchaseProducts,
                    cp => cp.Product.Id,
                    pp => pp.ProductId,
                    (cp, pp) => new { cp.Category, cp.Product, PurchaseProduct = pp }
                )
                .Join(
                    _context.Purchases,
                    cpp => cpp.PurchaseProduct.PurchaseId,
                    pu => pu.Id,
                    (cpp, pu) => new { cpp.Category, cpp.Product, cpp.PurchaseProduct, Purchase = pu }
                )
                .Where(x => x.Purchase.UserId == userId)
                .GroupBy(x => new { x.Product.Id, x.Product.Name })
                .Select(g => new ProductDto
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    Expenses = g.Sum(x => x.PurchaseProduct.Quantity * x.Product.Price)
                })
                .ToListAsync();
        }

        //public async Task<ProductsCategoriesDto> CreateConnection(ProductsCategoriesDto productsCategories)
        //{
        //    //Check category or product if exist???
        //    _context.ProductsCategories.Add(productsCategories);
        //}

        //public async Task<ProductsCategoriesDto> DeleteConnectionAsync(ProductsCategoriesDto productsCategoriesDto)
        //{
        //    //Check category or product if exist???
        //    _context.ProductsCategories.Remove(productsCategoriesDto.CategoryId);
        //    await _context.SaveChangesAsync();

        //    return productsCategoriesDto;
        //}
    }
}
