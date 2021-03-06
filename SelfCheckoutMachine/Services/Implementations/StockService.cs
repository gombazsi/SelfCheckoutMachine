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
            if (currencyCode == null)
            {
                currencyCode = _currencyService.HufCode;
            }
            else
            {
                await _currencyService.GetCurrencyByCode(currencyCode);
            }
            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc(currencyCode);
            return GetDictionaryFromList(stocksSaved, currencyCode);
        }

        public async Task<List<Stock>> GetStocksOrderedByDenominationDesc(string currencyCode)
        {
            return await _stockRepository.GetStocksOrderedByDenominationDesc(currencyCode);
        }
        public async Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks, string currencyCode)
        {
            if(currencyCode == null)
            {
                currencyCode = _currencyService.HufCode;
            }
            _currencyService.ValidateInput(stocks, currencyCode);
            Guid currencyId = _currencyService.HufId;
            if(currencyCode != _currencyService.HufCode)
            {
                currencyId = (await _currencyService.GetCurrencyByCode(currencyCode)).Id;
            }

            List<Stock> stocksSaved = await InsertStocks(stocks, currencyId);
            return GetDictionaryFromList(stocksSaved, currencyCode);
        }

        public async Task SaveChanges()
        {
            await _stockRepository.SaveChanges();
        }
        public Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList, string currencyCode = null)
        {
            if(currencyCode == null)
            {
                currencyCode = _currencyService.HufCode;
            }
            Dictionary<string, int> stocks = new Dictionary<string, int>();
            foreach (Stock stock in stockList)
            {
                string denomination = currencyCode == _currencyService.HufCode ? 
                    string.Format("{0:0}", stock.Denomination) : 
                    string.Format("{0:0.##}", stock.Denomination);
                stocks.Add(denomination, stock.Amount);
            }
            return stocks;
        }

        private async Task<Stock> CreateStock(decimal denomination, int amount, Guid currencyId)
        {
            Stock created = new Stock { Denomination = denomination, Amount = amount, CurrencyId = currencyId };
            await _stockRepository.CreateStock(created);
            return created;
        }

        public async Task<List<Stock>> InsertStocks(Dictionary<string, int> stocks, Guid currencyId)
        {
            List<Stock> stocksSaved = await GetStocksOrderedByDenominationDesc(_currencyService.HufCode);
            foreach (KeyValuePair<string, int> stock in stocks)
            {
                await InsertStock(stocksSaved, stock, currencyId);
            }
            await SaveChanges();
            return stocksSaved;
        }

        private async Task InsertStock(List<Stock> stocks, KeyValuePair<string, int> stock, Guid currencyId)
        {
            decimal denomination = Convert.ToDecimal(stock.Key);
            Stock stockToUpdate = stocks.FirstOrDefault(s => s.Denomination == denomination && s.CurrencyId == currencyId);
            if (stockToUpdate == null)
            {
                Stock createdStock = await CreateStock(denomination, stock.Value, currencyId);
                stocks.Add(createdStock);
            }
            else
            {
                stockToUpdate.Amount += stock.Value;
            }
        }
    }
}
