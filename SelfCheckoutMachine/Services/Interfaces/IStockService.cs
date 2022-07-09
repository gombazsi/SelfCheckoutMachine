using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface IStockService
    {
        /**
         * <summary>Returns response for GetStocks API method. Queries stocks of the given currency from the database.</summary>
         * <param name="currencyCode">Code of the stocks being queried</param>
         */
        public Task<Dictionary<string, int>> GetStockDictionary(string currencyCode);
        /**
         * <summary>Returns response for PostStocks API method. Returns stocks of the given currency from the database.</summary>
         * <param name="stocks">Stocks being inserted</param>
         * <param name="currencyCode">Code of the stocks being inserted.</param>
         */
        public Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks, string currencyCode);
        /**
         *<summary>Queries stocks of the given currency from the database, ordered by denomination descending.</summary>
         *<param name="currencyCode">Code of the stocks being queried</param>
         */
        public Task<List<Stock>> GetStocksOrderedByDenominationDesc(string currencyCode);
        /**
         * <summary>Persists changes to the database.</summary>
         */
        public Task SaveChanges();
        /**
         * <summary>Creates Dictionary of a list of stocks, used for returning API responses.</summary>
         * <param name="stockList">List of stocks being converted to dictionary</param>
         * <param name="currencyCode">Code of the currency used. Replaced by HUF if null is passed.</param>
         */
        public Dictionary<string, int> GetDictionaryFromList(List<Stock> stockList, string currencyCode = null);
        /**
         * <summary>Inserts stocks to the database</summary>
         * <param name="stocks">Stocks being inserted</param>
         * <param name="currencyId">Id of the currency being inserted</param>
         */
        public Task<List<Stock>> InsertStocks(Dictionary<string, int> stocks, Guid currencyId);
    }
}
