using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface IStockService
    {
        public Task<Dictionary<string, int>> GetStockDictionary();
        public Task<Dictionary<string, int>> PostStocks(Dictionary<string, int> stocks);
    }
}
