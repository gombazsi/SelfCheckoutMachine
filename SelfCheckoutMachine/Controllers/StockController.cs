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
        public async Task<Dictionary<string, int>> GetStocks()
        {
            return await _stockService.GetStockDictionary();
        }

        [HttpPost]
        public async Task<Dictionary<string, int>> PostStocks([FromBody] Dictionary<string, int> stocks)
        {
            return await _stockService.PostStocks(stocks);
        }
    }
}