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
                Dictionary<string, int> stocks = await _stockService.GetStockDictionary(currencyCode);
                _logger.LogInformation($"Successfully queried {currencyCode ?? "HUF"} balance.");
                return Ok(stocks);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error querying {currencyCode ?? "HUF"} balance: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<string, int>>> PostStocks([FromBody] Dictionary<string, int> stocks, string currencyCode = null)
        {
            try
            {
                Dictionary<string, int> stocksSaved = await _stockService.PostStocks(stocks, currencyCode);
                _logger.LogInformation($"Successfully updated {currencyCode ?? "HUF"} balance.");
                return Ok(stocksSaved);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating {currencyCode ?? "HUF"} balance: {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}