using SelfCheckoutMachine.Data.Enums;

namespace SelfCheckoutMachine.Data.Models
{
    public class Checkout
    {
        public Dictionary<HufDenominations, int> Inserted { get; set; }
        public int Price { get; set; }
    }
}
