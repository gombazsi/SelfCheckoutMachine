using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Data.Repositories.Interfaces;

namespace SelfCheckoutMachine.Data.Repositories.Implementations
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _dbContext;
        public CurrencyRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<Currency> GetCurrencyByCode(string currencyCode)
        {
            return await _dbContext.Currencies.FirstOrDefaultAsync(c => c.Code == currencyCode);
        }
    }
}
