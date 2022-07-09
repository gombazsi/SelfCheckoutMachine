using SelfCheckoutMachine.Data.Enums;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface ICheckoutService
    {
        public Task<Dictionary<string, int>> PostCheckout(Checkout checkout);
    }
}
