using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Interfaces
{
    public interface ISalesDataService
    {
        IEnumerable<SalesData> GetSalesData(string relativePath);
    }
}
