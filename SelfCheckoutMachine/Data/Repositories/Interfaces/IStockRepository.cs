using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Repositories.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocks();
    }
}
