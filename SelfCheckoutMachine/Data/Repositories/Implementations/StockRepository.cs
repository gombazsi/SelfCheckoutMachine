using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.Data.Models;
using SelfCheckoutMachine.Data.Repositories;
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

        public async Task CreateStock(Stock stock)
        {
            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
