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
        public async Task<ActionResult<Dictionary<string, int>>> PostCheckout([FromBody] Checkout checkout)
        {
            try
            {
                Dictionary<string, int> change = await _checkoutService.PostCheckout(checkout);
                _logger.LogInformation($"Payment of {checkout.Price} completed successfully.");
                return Ok(change);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error during checkout: {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
