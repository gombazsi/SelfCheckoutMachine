using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Services.Interfaces;

namespace SelfCheckoutMachine.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IStockService _stockService;
        public CheckoutService(IStockService stockService)
        {
            _stockService = stockService;
        }
        public async Task<Dictionary<string, int>> PostCheckout(Checkout checkout)
        {
            int paidAmount = checkout.Inserted.ToList().Select(s => s.Value * (int)s.Key).Sum();
            if(paidAmount < checkout.Price)
            {
                throw new Exception("Insert more money to complete purchase.");
            }
            List<Stock> stocks = await _stockService.GetStocksOrderedByDenominationDesc();

            List<Stock> change = GetChange(stocks, paidAmount - checkout.Price);
            await _stockService.PostStocks(checkout.Inserted);
            await _stockService.SaveChanges();
            return _stockService.GetDictionaryFromList(change);
        }
        private List<Stock> GetChange(List<Stock> stocks, int price)
        {
            List<Stock> change = new List<Stock>();

            while(price > 0)
            {
                Stock largest = stocks.FirstOrDefault(s => (int)s.Denomination <= price && s.Amount > 0);
                if (largest == null)
                {
                    throw new Exception("Machine ran out of change. Please try smaller denominations!");
                }
                int denomination = (int)largest.Denomination;
                int amount = price / denomination;
                change.Add(new Stock { Denomination = largest.Denomination, Amount = amount });
                largest.Amount -= amount;
                if(largest.Amount == 0)
                {
                    stocks.Remove(largest);
                }
                price -= amount * denomination;
            }
            return change;
        }
    }
}
