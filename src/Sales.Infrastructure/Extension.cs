using Microsoft.Extensions.DependencyInjection;
using Sales.Core.Application.Interfaces;
using Sales.Infrastructure.Repository;

namespace Sales.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddSalesInfrastructureDI(this IServiceCollection services)
        {
            services.AddScoped<ISalesRepository, SalesRepository>();
            return services;
        }
    }
}
