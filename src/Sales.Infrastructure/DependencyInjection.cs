using Microsoft.Extensions.DependencyInjection;
using Sales.Core.Application.Interfaces;
using Sales.Core.Application.Services;
using Sales.Infrastructure.Repository;

namespace Sales.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSalesInfrastructureDI(this IServiceCollection services)
        {
            services.AddScoped<ISalesDataRepository, SalesDataRepository>();
            services.AddScoped<ISalesDataService, SalesDataService>();
            return services;
        }
    }
}
