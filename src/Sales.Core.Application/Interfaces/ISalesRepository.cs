using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Interfaces
{

    public interface ISalesRepository
    {
        IEnumerable<SalesData> GetSalesData(string filePath);
    }
}
