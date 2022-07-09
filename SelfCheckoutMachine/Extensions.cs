using SelfCheckoutMachine.Data.Repositories;
using SelfCheckoutMachine.Data.Repositories.Implementations;
using SelfCheckoutMachine.Data.Repositories.Interfaces;
using SelfCheckoutMachine.Repositories.Implementations;
using SelfCheckoutMachine.Repositories.Interfaces;
using SelfCheckoutMachine.Services.Implementations;
using SelfCheckoutMachine.Services.Interfaces;
using System.ComponentModel;
using System.Reflection;

namespace SelfCheckoutMachine
{
    public static class Extensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddTransient<IStockService, StockService>()
                .AddTransient<ICurrencyService, CurrencyService>()
                .AddTransient<ICheckoutService, CheckoutService>()
                .AddTransient<ICurrencyRepository, CurrencyRepository>()
                .AddTransient<IStockRepository, StockRepository>();
            builder.Services.AddDbContext<AppDbContext>();
        }
    }
}
