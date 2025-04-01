using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Interfaces
{

    public interface ISalesDataRepository
    {
        IEnumerable<SalesData> GetSalesData(string filePath);
    }
}
