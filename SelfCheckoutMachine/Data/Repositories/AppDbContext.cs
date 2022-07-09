using Microsoft.EntityFrameworkCore;
using SelfCheckoutMachine.Data.Models;

namespace SelfCheckoutMachine.Data.Repositories
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .Property(c => c.ValueInHuf)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Stock>()
                .Property(s => s.Denomination)
                .HasPrecision(8, 2);
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
