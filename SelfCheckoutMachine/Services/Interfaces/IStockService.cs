using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface IStockService
    {
        public Task<Dictionary<string, int>> GetStockDictionary();
        public Task<List<Stock>> GetStocksOrderedByDenominationDesc();
        public Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks);
        public Task SaveChanges();
        public Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList);
        public Task PostStocks(Dictionary<HufDenominations, int> inserted);
    }
}
