using AlbertCalculator.Data;
using AlbertCalculator.Models;
using AlbertCalculator.Utils;
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

        public async Task<List<PurchaseProducts>> CreatePurchaseProductsAsync(List<ParsedProduct> products, Purchase purchase)
        {
            // Collect product codes
            List<int> productCodes = products.Select(p => p.Code).ToList();

            // Find all product Ids in DB
            Dictionary<int, Guid> productsFromDb = await _context.Products
                .Where(p => productCodes.Contains(p.Code))
                .ToDictionaryAsync(p => p.Code, p => p.Id);

            // Create PurchaseProducts List
            List<PurchaseProducts> productPurchases = products
                .Where(p => productsFromDb.ContainsKey(p.Code))
                .Select(p => new PurchaseProducts
                {
                    PurchaseId = purchase.Id,
                    ProductId = productsFromDb[p.Code],
                    Quantity = p.Quantity,
                    Discount = p.Discount
                })
                .ToList();

            // Add new records and save in DB
            await _context.PurchaseProducts.AddRangeAsync(productPurchases);
            await _context.SaveChangesAsync();

            return productPurchases;
        }

        public async Task DeletePurchaseProductsAsync(Guid purchaseId)
        {
            // Search for existed PP contains purchaseId
            List<PurchaseProducts> existingPurchaseProducts = await _context.PurchaseProducts
                .Where(pp => pp.PurchaseId == purchaseId)
                .ToListAsync();

            _context.PurchaseProducts.RemoveRange(existingPurchaseProducts);
            await _context.SaveChangesAsync();
        }
    }
}
