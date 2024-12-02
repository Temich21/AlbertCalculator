using AlbertCalculator.Data;
using AlbertCalculator.Dtos;
using AlbertCalculator.Models;

namespace AlbertCalculator.Repositories
{
    public class PurchaseRepository
    {
        private readonly AlbertCalculatorDataContext _context;

        public PurchaseRepository(AlbertCalculatorDataContext context)
        {
            _context = context;
        }

        public async Task<Purchase> CreatePurchaseAsync(PurchaseDto purchaseDto)
        {
            Purchase newPurchase = new Purchase{
                Id = purchaseDto.Id,
                Date = DateTime.Parse(purchaseDto.Date),
                Name = purchaseDto.Name,
                UserId = purchaseDto.UserId,
            };

            _context.Purchases.Add(newPurchase);
            await _context.SaveChangesAsync();

            return newPurchase;
        }

        public async Task<Purchase> UpdatePurchaseAsync(PurchaseDto purchaseDto)
        {
            Purchase updatePurchase = new Purchase
            {
                Id = purchaseDto.Id,
                Date = DateTime.Parse(purchaseDto.Date),
                Name = purchaseDto.Name,
                UserId = purchaseDto.UserId,
            };

            _context.Purchases.Update(updatePurchase);
            await _context.SaveChangesAsync();

            return updatePurchase;
        }
    }
}
