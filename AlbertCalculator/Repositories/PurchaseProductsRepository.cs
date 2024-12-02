using AlbertCalculator.Data;
using AlbertCalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbertCalculator.Repositories
{
    public class PurchaseProductsRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public PurchaseProductsRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<Guid> FindProductIdByCodeAsync(int code)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Code == code);

            return product == null ? throw new KeyNotFoundException($"Product {code} doesn't exist.") : product.Id;
        }

        public async Task CreatePurchaseProducts(List<PurchaseProducts> purchaseProductsList)
        {
            _context.PurchaseProducts.AddRange(purchaseProductsList);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePurchaseProductsAsync(Guid purchaseId)
        {
            List<PurchaseProducts> existingPurchaseProducts = await _context.PurchaseProducts.Where(pp => pp.PurchaseId == purchaseId).ToListAsync();
            _context.PurchaseProducts.RemoveRange(existingPurchaseProducts);

            await _context.SaveChangesAsync();
        }
    }
}
