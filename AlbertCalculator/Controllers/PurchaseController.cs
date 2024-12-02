using Microsoft.AspNetCore.Mvc;
using AlbertCalculator.Dtos;
using AlbertCalculator.Service;

namespace AlbertCalculator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;

        public PurchaseController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost()]
        public async Task<ActionResult<PurchaseDto>> CreatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            var result = await _purchaseService.CreatePurchase(purchaseDto);
            
            return Ok(result);
        }

        [HttpPatch()]
        public async Task<ActionResult<PurchaseDto>> UpdatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            var result = await _purchaseService.UpdatePurchase(purchaseDto);

            return Ok(result);
        }
    }
}
