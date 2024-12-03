using AlbertCalculator.Dtos;
using AlbertCalculator.Models;
using AlbertCalculator.Repositories;
using AlbertCalculator.Utils;
namespace AlbertCalculator.Service
{
    public class PurchaseService
    {
        private readonly PurchaseRepository _purchaseRepository;
        private readonly PurchaseProductsRepository _purchaseProductsRepository;
        private readonly FileRepository _fileRepository;

        public PurchaseService(PurchaseRepository purchaseRepository, PurchaseProductsRepository purchaseProductsRepository, FileRepository fileRepository)
        {
            _purchaseRepository = purchaseRepository;
            _purchaseProductsRepository = purchaseProductsRepository;
            _fileRepository = fileRepository;
        }

        public async Task<PurchaseDto> CreatePurchase(PurchaseDto purchaseDto)
        {
            // Recognize text from PDF
            string pdfText = ParsePurchase.DecodePDF(purchaseDto.File.Data);

            // Parse products from text
            List<ParsedProduct> products = ParsePurchase.ParseProducts(pdfText);

            // Save purchase, file and pp in DB
            Purchase purchase = await _purchaseRepository.CreatePurchaseAsync(purchaseDto);

            await _fileRepository.CreateFileAsync(purchaseDto.File);

            await _purchaseProductsRepository.CreatePurchaseProductsAsync(products, purchase);

            return purchaseDto;
        }

        public async Task<PurchaseDto> UpdatePurchase(PurchaseDto purchaseDto)
        {
            Purchase purchase = await _purchaseRepository.UpdatePurchaseAsync(purchaseDto);

            FileModel? file = await _fileRepository.FindFileAsync(purchaseDto.File.Id) 
                ?? throw new KeyNotFoundException($"File with ID {purchaseDto.File.Id} doesn't exist.");

            if (file.Data != purchaseDto.File.Data)
            {
                // Delete all related PP in database
                await _purchaseProductsRepository.DeletePurchaseProductsAsync(purchaseDto.Id);

                // Parse PDF
                string pdfText = ParsePurchase.DecodePDF(purchaseDto.File.Data);

                List<ParsedProduct> products = ParsePurchase.ParseProducts(pdfText);

                // Save pp in DB
                await _purchaseProductsRepository.CreatePurchaseProductsAsync(products, purchase);
            }

            return purchaseDto;
        }

        public async Task DeletePurchase(Guid purchaseId)
        {
            Purchase? purchase = await _purchaseRepository.FindOneByIdAsync(purchaseId) 
                ?? throw new KeyNotFoundException($"Purchase with ID {purchaseId} doesn't exist.");

            await _purchaseRepository.DeletePurchaseAsync(purchase);
        }
    }
}
