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
            Guid currencyId = new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba");
            if (checkout.CurrencyCode != "HUF")
            {
                Currency currency = await _currencyService.GetCurrencyByCode(checkout.CurrencyCode);
                valueInHuf = currency.ValueInHuf;
                currencyId = currency.Id;
            }

            decimal paidAmount = checkout.Inserted.ToList().Select(s => s.Value * (int)s.Key * valueInHuf).Sum();
            if (paidAmount < checkout.Price)
            {
                throw new Exception("Insert more money to complete purchase.");
            }
            List<Stock> stocks = await _stockService.GetStocksOrderedByDenominationDesc("HUF");

            List<Stock> change = GetChange(stocks, paidAmount - checkout.Price, checkout.CurrencyCode == "HUF");
            await _stockService.PostStocks(checkout.Inserted, checkout.CurrencyCode);
            await _stockService.SaveChanges();
            return _stockService.GetDictionaryFromList(change);

        }
        private List<Stock> GetChange(List<Stock> stocks, decimal price, bool exactChange = true)
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
                    int denomination = (int)largest.Denomination;
                    int amount = Convert.ToInt32(price / denomination);
                    change.Add(new Stock { Denomination = largest.Denomination, Amount = amount, CurrencyId = new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba" )});
                    largest.Amount -= amount;
                    if (largest.Amount == 0)
                    {
                        stocks.Remove(largest);
                    }
                    price -= amount * denomination;
                }                
            }
            return change;
        }
    }
}
