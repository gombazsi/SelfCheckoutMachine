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
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
