using Microsoft.AspNetCore.Mvc;
using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;
        public StockController(ILogger<StockController> logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, int>>> GetStocks([FromQuery] string currencyCode = null)
        {
            try
            {
                return Ok(await _stockService.GetStockDictionary(currencyCode));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<string, int>>> PostStocks([FromBody] Dictionary<string, int> stocks, string currencyCode = null)
        {
            try
            {
                return Ok(await _stockService.PostStocks(stocks, currencyCode));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}