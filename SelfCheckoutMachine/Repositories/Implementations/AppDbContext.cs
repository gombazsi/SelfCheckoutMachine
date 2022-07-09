using Microsoft.EntityFrameworkCore;

namespace SelfCheckoutMachine.Repositories.Implementations
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("SelfCheckoutMachineDb"));
        }

        public DbSet<Stock> Stocks { get; set; }
    }
}
