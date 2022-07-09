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
            List<Stock> stocksSaved = await GetStocks();
            return GetDictionaryFromList(stocksSaved);
        }

        public async Task<List<Stock>> GetStocks()
        {
            return await _stockRepository.GetStocks();
        }

        public async Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks)
        {
            List<Stock> stocksSaved = await GetStocks(); //maybe use filtered get method
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

        private HufDenominations GetHufDenominations(string value)
        {
            int valueInt = Convert.ToInt32(value);
            return (HufDenominations)valueInt;
            //exception
        }

        private Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList)
        {
            Dictionary<string, int> stocks = new Dictionary<string, int>();
            foreach (Stock stock in stockList)
            {
                stocks.Add(stock.Denomination.GetDescription(), stock.Amount);
            }
            return stocks;
        }
    }
}
