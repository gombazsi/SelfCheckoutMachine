using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Repositories.Interfaces;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ICurrencyService _currencyService;
        public StockService(IStockRepository stockRepository, ICurrencyService currencyService)
        {
            _stockRepository = stockRepository;
            _currencyService = currencyService;
        }
        public async Task<Dictionary<string, int>> GetStockDictionary(string currencyCode)
        {
            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc(currencyCode);
            return GetDictionaryFromList(stocksSaved);
        }
        public async Task<List<Stock>> GetStocksOrderedByDenominationDesc(string currencyCode)
        {
            return await _stockRepository.GetStocksOrderedByDenominationDesc(currencyCode);
        }
        public async Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks, string currencyCode)
        {
            Guid currencyId = (await _currencyService.GetCurrencyByCode(currencyCode)).Id;

            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc(currencyCode);
            foreach (KeyValuePair<string, int> stock in stocks)
            {
                decimal denomination = Convert.ToDecimal(stock.Key);
                //exception
                Stock stockToUpdate = stocksSaved.FirstOrDefault(s => s.Denomination == denomination && s.Currency.Code == currencyCode);
                if (stockToUpdate == null)
                {
                    Stock created = new Stock { Denomination = denomination, Amount = stock.Value, CurrencyId = currencyId };
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
                stocks.Add(stock.Denomination.ToString(), stock.Amount);
                //exception
            }
            return stocks;
        }

        public async Task PostStocks(Dictionary<decimal, int> inserted, string currencyCode)
        {
            Dictionary<string, int> stocks = new Dictionary<string, int>();
            foreach (KeyValuePair<decimal, int> stock in inserted)
            {
                stocks.Add(stock.Key.ToString(), stock.Value);
            }
            await PostStocks(stocks, currencyCode);
        }
    }
}
