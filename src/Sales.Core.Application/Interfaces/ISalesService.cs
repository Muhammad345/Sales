using Sales.Core.Application.DTO_Entities;
using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Interfaces
{
    public interface ISalesService
    {
        IEnumerable<SalesSummaryDto> GetSalesSummary();
        IEnumerable<SalesDto> GetSalesDetail();
    }
}
