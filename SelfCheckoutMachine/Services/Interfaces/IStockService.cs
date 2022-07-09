using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface IStockService
    {
        public Task<Dictionary<string, int>> GetStockDictionary();
        public Task<List<Stock>> PostStocks(Dictionary<HufDenominations, int> stocks);
    }
}
