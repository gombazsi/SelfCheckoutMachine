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
        public async Task<Dictionary<string, int>> GetStocks([FromQuery] string currencyCode = null)
        {
            return await _stockService.GetStockDictionary(currencyCode);
        }

        [HttpPost]
        public async Task<Dictionary<string, int>> PostStocks([FromBody] Dictionary<string, int> stocks, string currencyCode = null)
        {
            return await _stockService.PostStocks(stocks, currencyCode);
        }
    }
}