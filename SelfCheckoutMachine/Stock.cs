namespace SelfCheckoutMachine
{
    public class Stock
    {
        public Guid Id { get; set; }
        public HufDenominations Denomination { get; set; }
        public int Amount { get; set; }
    }
}
