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

            modelBuilder.Entity<Currency>()
                .HasData(
                    new Currency
                    {
                        Id = new Guid("960de4c0-bbbe-4e74-b4d5-4c1754f4f9ba"),
                        Code = "HUF",
                        ValueInHuf = 1
                    },
                    new Currency
                    {
                        Id = new Guid("e8547fe0-e517-45b3-91c1-0e12772211cc"),
                        Code = "EUR",
                        ValueInHuf = 407.21m
                    }
                );
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
