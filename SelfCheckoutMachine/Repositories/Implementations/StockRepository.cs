using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.Repositories.Interfaces;

namespace SelfCheckoutMachine.Repositories.Implementations
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _dbContext;
        public StockRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<List<Stock>> GetStocks()
        {
            List<Stock> stocksSaved = await _dbContext.Stocks.ToListAsync();

            return stocksSaved;
        }
    }
}
