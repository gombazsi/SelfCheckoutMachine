using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Repositories.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocks();
        public Task CreateStock(Stock stock);
        public Task SaveChanges();
    }
}
