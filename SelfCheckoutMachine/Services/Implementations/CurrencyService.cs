using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Data.Repositories.Interfaces;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly string HufCode = "HUF";
        private readonly string EurCode = "EUR";

        private readonly List<decimal> HufDenominations = new List<decimal> { 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000 };
        private readonly List<decimal> EurDenominations = new List<decimal> { 0.01m, 0.02m, 0.05m, 0.1m, 0.2m, 0.5m, 1, 2, 5, 10, 20, 50, 100, 200, 500 };

        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public void ValidateInput(List<Stock> stocks)
        {
            foreach (Stock stock in stocks)
            {
                List<decimal> denominations = null;
                if (stock.Currency.Code == HufCode)
                {
                    denominations = HufDenominations;
                }
                else if (stock.Currency.Code == EurCode)
                {
                    denominations = EurDenominations;
                }
                if (denominations == null || !denominations.Contains(stock.Denomination))
                {
                    throw new Exception("Invalid input!");
                }
            }
        }

        public async Task<Currency> GetCurrencyByCode(string currencyCode)
        {
            Currency currency = await _currencyRepository.GetCurrencyByCode(currencyCode);
            if (currency == null)
            {
                throw new Exception("Unknown currency.");
            }
            return currency;
        }
    }
}
