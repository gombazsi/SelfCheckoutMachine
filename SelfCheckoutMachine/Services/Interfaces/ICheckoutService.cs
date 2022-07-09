using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface ICheckoutService
    {
        /**
         * <summary>Simulates a new checkout</summary>
         * <param name="checkout">Contains price, inserted stocks and optional currency. Default currency is HUF.</param>
         */
        public Task<Dictionary<string, int>> PostCheckout(Checkout checkout);
    }
}
