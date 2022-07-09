using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Data.Repositories.Interfaces;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        public string HufCode => "HUF";
        public string EurCode => "EUR";
        public Guid HufId => new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba");

        private readonly List<decimal> HufDenominations = new List<decimal> { 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000 };
        private readonly List<decimal> EurDenominations = new List<decimal> { 0.01m, 0.02m, 0.05m, 0.1m, 0.2m, 0.5m, 1, 2, 5, 10, 20, 50, 100, 200, 500 };

        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<Currency> GetCurrencyByCode(string currencyCode)
        {
            Currency currency = await _currencyRepository.GetCurrencyByCode(currencyCode);
            if (currency == null)
            {
                throw new Exception($"Unknown currency: {currencyCode}.");
            }
            return currency;
        }

        public void ValidateInput(Dictionary<string, int> stocks, string currencyCode)
        {
            foreach (KeyValuePair<string, int> inputStock in stocks.ToList())
            {
                decimal denomination = 0;
                try
                {
                    denomination = Convert.ToDecimal(inputStock.Key);
                }
                catch
                {
                    throw new Exception($"Error parsing input denomination: {inputStock.Key} {currencyCode}.");
                }
                if(inputStock.Value <= 0)
                {
                    throw new Exception($"Cannot insert {inputStock.Value} amount of {inputStock.Key} {currencyCode}.");
                }

                List<decimal> allowedDenominations = GetAllowedDenominations(currencyCode);
                if (!allowedDenominations.Contains(denomination))
                {
                    throw new Exception($"Error parsing input denomination: {inputStock.Key} {currencyCode}.");
                }
            }
        }

        private List<decimal> GetAllowedDenominations(string currencyCode)
        {
            if(currencyCode == HufCode)
            {
                return HufDenominations;
            }
            else if(currencyCode == EurCode)
            {
                return EurDenominations;
            }
            else
            {
                throw new Exception($"Unknown currency: {currencyCode}.");
            }
        }
    }
}
