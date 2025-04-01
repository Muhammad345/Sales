using Microsoft.Extensions.DependencyInjection;

namespace Sales.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSalesCoreApplicationDI(this IServiceCollection services)
        {
            return services;
        }
    }
}
