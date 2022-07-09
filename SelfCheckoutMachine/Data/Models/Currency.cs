namespace SelfCheckoutMachine.Data.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal ValueInHuf { get; set; }
    }
}
