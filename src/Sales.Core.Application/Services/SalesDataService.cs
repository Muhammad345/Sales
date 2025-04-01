using Sales.Core.Application.Interfaces;
using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Services
{
    public class SalesDataService(ISalesDataRepository salesDataRepository) : ISalesDataService
    {
        private readonly ISalesDataRepository _salesDataRepository = salesDataRepository;

        public IEnumerable<SalesData> GetSalesData(string relativePath)
        {
            return _salesDataRepository.GetSalesData(relativePath);
        }
    }
}
