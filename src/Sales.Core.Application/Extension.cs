using Microsoft.Extensions.DependencyInjection;
using Sales.Core.Application.Interfaces;
using Sales.Core.Application.Services;

namespace Sales.Core.Application
{
    public static class Extension
    {
        public static IServiceCollection AddSalesCoreApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<ISalesService, SalesService>();
            return services;
        }
    }
}
