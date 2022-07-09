using SelfCheckoutMachine.Data.Enums;

namespace SelfCheckoutMachine.Data.Models
{
    public class Stock
    {
        public Guid Id { get; set; }
        public decimal Denomination { get; set; }
        public int Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
