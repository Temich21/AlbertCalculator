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

            // Save purchase and file in DB
            Purchase purchase = await _purchaseRepository.CreatePurchaseAsync(purchaseDto);

            await _fileRepository.CreateFileAsync(purchaseDto.File);

            // Create and Save PurchaseProducts list
            List<PurchaseProducts> productPurchases = [];

            foreach (ParsedProduct product in products)
            {
                Guid productId = await _purchaseProductsRepository.FindProductIdByCodeAsync(product.Code);

                PurchaseProducts purchaseProduct = new PurchaseProducts
                {
                    PurchaseId = purchase.Id,
                    ProductId = productId,
                    Quantity = product.Quantity,
                    Discount = product.Discount
                };

                productPurchases.Add(purchaseProduct);
            }

            await _purchaseProductsRepository.CreatePurchaseProducts(productPurchases);

            // Не будет ли лучше создавать инстансы и потом уже сохранять?
            return purchaseDto;
        }

        public async Task<PurchaseDto> UpdatePurchase(PurchaseDto purchaseDto)
        {
            Purchase purchase = await _purchaseRepository.UpdatePurchaseAsync(purchaseDto);

            FileModel file = await _fileRepository.FindFileAsync(purchaseDto.File.Id);

            if (file.Data != purchaseDto.File.Data)
            {
                // Delete all related PP in database
                await _purchaseProductsRepository.DeletePurchaseProductsAsync(purchaseDto.Id);

                // Parse PDF
                string pdfText = ParsePurchase.DecodePDF(purchaseDto.File.Data);

                List<ParsedProduct> products = ParsePurchase.ParseProducts(pdfText);

                List<PurchaseProducts> productPurchases = [];

                foreach (ParsedProduct product in products)
                {
                    Guid productId = await _purchaseProductsRepository.FindProductIdByCodeAsync(product.Code);

                    PurchaseProducts purchaseProduct = new PurchaseProducts
                    {
                        PurchaseId = purchase.Id,
                        ProductId = productId,
                        Quantity = product.Quantity,
                        Discount = product.Discount
                    };

                    productPurchases.Add(purchaseProduct);
                }

                await _purchaseProductsRepository.CreatePurchaseProducts(productPurchases);
            }

            return purchaseDto;
        }



    }
}
