using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;
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
            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc();
            return GetDictionaryFromList(stocksSaved);
        }
        public async Task<List<Stock>> GetStocksOrderedByDenominationDesc()
        {
            return await _stockRepository.GetStocksOrderedByDenominationDesc();
        }
        public async Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks)
        {
            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc(); //maybe use filtered get method
            foreach (KeyValuePair<string, int> stock in stocks)
            {
                HufDenominations denomination = GetHufDenominations(stock.Key);
                Stock stockToUpdate = stocksSaved.FirstOrDefault(s => s.Denomination == denomination);
                if (stockToUpdate == null)
                {
                    //save every coin/bill to db which doesnt throw error
                    Stock created = new Stock { Denomination = denomination, Amount = stock.Value };
                    await _stockRepository.CreateStock(created);
                    stocksSaved.Add(created);
                }
                else
                {
                    stockToUpdate.Amount += stock.Value;
                }
            }
            await _stockRepository.SaveChanges();
            return GetDictionaryFromList(stocksSaved);
        }
        public async Task SaveChanges()
        {
            await _stockRepository.SaveChanges();
        }
        public Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList)
        {
            Dictionary<string, int> stocks = new Dictionary<string, int>();
            foreach (Stock stock in stockList)
            {
                stocks.Add(stock.Denomination.GetDescription(), stock.Amount);
            }
            return stocks;
        }
        private HufDenominations GetHufDenominations(string value)
        {
            int valueInt = Convert.ToInt32(value);
            return (HufDenominations)valueInt;
            //exception
        }
        public async Task PostStocks(Dictionary<HufDenominations, int> inserted)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            foreach (var item in inserted)
            {
                keyValuePairs.Add(item.Key.GetDescription(), item.Value);
            }
            await PostStocks(keyValuePairs);
        }
    }
}
