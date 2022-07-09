using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly ICheckoutService _checkoutService;
        public CheckoutController(ILogger<StockController> logger, ICheckoutService checkoutService)
        {
            _logger = logger;
            _checkoutService = checkoutService;
        }


        [HttpPost]
        public async Task<Dictionary<string, int>> PostCheckout([FromBody] Checkout checkout)
        {
            return await _checkoutService.PostCheckout(checkout);
        }
    }
}
