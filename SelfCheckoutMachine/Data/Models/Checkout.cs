using SelfCheckoutMachine.Data.Enums;

namespace SelfCheckoutMachine.Data.Models
{
    public class Checkout
    {
        public Dictionary<string, int> Inserted { get; set; }
        public decimal Price { get; set; }
        public string CurrencyCode { get; set; }
    }
}
