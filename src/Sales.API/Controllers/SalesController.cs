using Microsoft.AspNetCore.Mvc;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain.Model;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController(ILogger<SalesController> logger, ISalesDataService salesDataService) : ControllerBase
    {

        private readonly ILogger<SalesController> _logger = logger;
        private readonly ISalesDataService _salesDataService = salesDataService;

        [HttpGet(Name = nameof(GetAllSales))]
        public IEnumerable<SalesData> GetAllSales()
        {
            return _salesDataService.GetSalesData(string.Empty);
        }

        [HttpGet(Name = nameof(GetSalesSummary))]
        public IEnumerable<SalesData> GetSalesSummary()
        {
            return _salesDataService.GetSalesData(string.Empty);
        }
    }
}
