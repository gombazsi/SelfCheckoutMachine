using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Data.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        public Task<Currency> GetCurrencyByCode(string currencyCode);
    }
}
