using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IStockService _stockService;
        private readonly ICurrencyService _currencyService;
        public CheckoutService(IStockService stockService, ICurrencyService currencyService)
        {
            _stockService = stockService;
            _currencyService = currencyService;
        }

        public async Task<Dictionary<string, int>> PostCheckout(Checkout checkout)
        {
            decimal valueInHuf = 1;
            Guid currencyId = _currencyService.HufId;
            if(checkout.CurrencyCode == null)
            {
                checkout.CurrencyCode = _currencyService.HufCode;
            }
            else if (checkout.CurrencyCode != _currencyService.HufCode)
            {
                Currency currency = await _currencyService.GetCurrencyByCode(checkout.CurrencyCode);
                valueInHuf = currency.ValueInHuf;
                currencyId = currency.Id;
            }

            decimal paidAmountInHuf = checkout.Inserted.ToList().Select(s => s.Value * (int)s.Key * valueInHuf).Sum();
            if (paidAmountInHuf < checkout.Price)
            {
                throw new Exception("Insert more money to complete purchase.");
            }
            List<Stock> stocks = await _stockService.GetStocksOrderedByDenominationDesc(_currencyService.HufCode);

            List<Stock> change = GetChangeInHuf(stocks, paidAmountInHuf - checkout.Price, checkout.CurrencyCode == _currencyService.HufCode);
            await _stockService.PostStocks(checkout.Inserted, checkout.CurrencyCode);
            await _stockService.SaveChanges();
            return _stockService.GetDictionaryFromList(change);

        }

        private List<Stock> GetChangeInHuf(List<Stock> stocks, decimal price, bool exactChange = true)
        {
            List<Stock> change = new List<Stock>();

            while(price > 0)
            {
                Stock largest = stocks.FirstOrDefault(s => (int)s.Denomination <= price && s.Amount > 0);
                if (largest == null)
                {
                    if (exactChange)
                    {
                        throw new Exception("Machine ran out of change. Please try smaller denominations!");
                    }
                    else
                    {
                        return change;
                    }
                }
                else
                {
                    int amount = Math.Min((int)(price / largest.Denomination), largest.Amount);
                    change.Add(new Stock { Denomination = largest.Denomination, Amount = amount, CurrencyId = _currencyService.HufId});
                    largest.Amount -= amount;
                    if (largest.Amount == 0)
                    {
                        stocks.Remove(largest);
                    }
                    price -= amount * largest.Denomination;
                }                
            }
            return change;
        }
    }
}
