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
            if(checkout.CurrencyCode == null)
            {
                checkout.CurrencyCode = _currencyService.HufCode;
            }
            _currencyService.ValidateInput(checkout.Inserted, checkout.CurrencyCode);
            decimal valueInHuf = 1;
            Guid currencyId = _currencyService.HufId;
            if (checkout.CurrencyCode != _currencyService.HufCode)
            {
                Currency currency = await _currencyService.GetCurrencyByCode(checkout.CurrencyCode);
                valueInHuf = currency.ValueInHuf;
                currencyId = currency.Id;
            }

            //get sum of paid money in huf
            decimal paidAmountInHuf = checkout.Inserted.ToList().Select(s => s.Value * Convert.ToDecimal(s.Key) * valueInHuf).Sum();
            if (paidAmountInHuf < checkout.Price)
            {
                throw new Exception("Insert more money to complete purchase.");
            }

            List<Stock> stocksSaved = await _stockService.GetStocksOrderedByDenominationDesc(_currencyService.HufCode);
            List<Stock> change = GetChangeInHuf(stocksSaved, paidAmountInHuf - checkout.Price, checkout.CurrencyCode == _currencyService.HufCode);
            await _stockService.InsertStocks(checkout.Inserted, currencyId);
            return _stockService.GetDictionaryFromList(change);

        }

        private List<Stock> GetChangeInHuf(List<Stock> stocks, decimal price, bool exactChange = true)
        {
            List<Stock> change = new List<Stock>();

            while (price > 0)
            {
                //get largest denomination
                Stock largest = stocks.FirstOrDefault(s => (int)s.Denomination <= price && s.Amount > 0);
                if (largest == null)
                {
                    if (exactChange)
                    {
                        throw new Exception("Machine ran out of change. Please try smaller denominations!");
                    }
                    else
                    {
                        //return returnable change stored in the machibe, which is inaccurate
                        return change;
                    }
                }
                else
                {
                    //get largest amount
                    int amount = Math.Min((int)(price / largest.Denomination), largest.Amount);
                    change.Add(new Stock { Denomination = largest.Denomination, Amount = amount, CurrencyId = _currencyService.HufId });
                    
                    //reduce amount stored in database
                    largest.Amount -= amount;
                    if (largest.Amount == 0)
                    {
                        stocks.Remove(largest);
                    }

                    //reduce remaining price
                    price -= amount * largest.Denomination;
                }
            }
            //accurately return change
            return change;
        }
    }
}
