using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface IStockService
    {
        public Task<Dictionary<string, int>> GetStockDictionary(string currencyCode);
        public Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks, string currencyCode);
        public Task<List<Stock>> GetStocksOrderedByDenominationDesc(string currencyCode);
        public Task SaveChanges();
        public Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList, string currencyCode = null);
        public Task<List<Stock>> InsertStocks(Dictionary<string, int> stocks, Guid currencyId);
    }
}
