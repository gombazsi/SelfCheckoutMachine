using SelfCheckoutMachine.Repositories.Interfaces;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<Dictionary<string, int>> GetStockDictionary()
        {
            List<Stock> stocksSaved = await GetStocks();
            Dictionary<string, int> stocks = new Dictionary<string, int>();
            foreach (Stock stock in stocksSaved)
            {
                stocks.Add(stock.Denomination.GetDescription(), stock.Amount);
            }
            return stocks;
        }

        public async Task<List<Stock>> GetStocks()
        {
            return await _stockRepository.GetStocks();
        }

        public Task<List<Stock>> PostStocks(Dictionary<HufDenominations, int> stocks)
        {
            throw new NotImplementedException();
        }
    }
}
