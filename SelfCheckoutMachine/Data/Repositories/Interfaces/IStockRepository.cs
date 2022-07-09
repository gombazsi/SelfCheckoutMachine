using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Repositories.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocksOrderedByDenominationDesc(string currencyCode);
        public Task CreateStock(Stock stock);
        public Task SaveChanges();
    }
}
