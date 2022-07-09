using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface ICurrencyService
    {
        public Task<Currency> GetCurrencyByCode(string currencyCode);
    }
}