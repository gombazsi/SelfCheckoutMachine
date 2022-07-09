using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Services.Interfaces
{
    public interface ICurrencyService
    {
        /**
         * <summary>Queries the database for a currency with the given code. Throws exception if no currencies are found.</summary>
         * <param name="currencyCode">Code of the currency being queried</param>
         */
        public Task<Currency> GetCurrencyByCode(string currencyCode);

        /**
         * <summary>Throws Exception if input has unknown denominations, or inserted amount is less than or equal to zero.</summary>
         * <param name="stocks">Stocks inserted</param>
         * <param name="currencyCode">Code of input currency</param>
         */
        public void ValidateInput(Dictionary<string, int> stocks, string currencyCode);
        /**
         * <summary>Id of the HUF currency stored in database</summary>
         */
        public Guid HufId { get; }
        /**
         * <summary>Code of the HUF currency stored in database</summary>
         */
        public string HufCode { get; }
        /**
         * <summary>Code of the EUR currency stored in database</summary>
         */
        public string EurCode { get; }
    }
}