using SelfCheckoutMachine.Data.Enums;

namespace SelfCheckoutMachine.Data.Models
{
    public class Stock
    {
        public Guid Id { get; set; }
        public HufDenominations Denomination { get; set; }
        public int Amount { get; set; }
    }
}
