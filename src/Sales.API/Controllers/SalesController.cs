using Microsoft.AspNetCore.Mvc;
using Sales.Core.Application.DTO_Entities;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain;
using Sales.Core.Domain.Model;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController(ILogger<SalesController> logger, ISalesService salesDataService) : ControllerBase
    {

        private readonly ILogger<SalesController> _logger = logger;
        private readonly ISalesService _salesDataService = salesDataService;

        [HttpGet(Name = nameof(GetSalesDetail))]
        public ActionResult<SalesApiResponse<IEnumerable<SalesDto>>> GetSalesDetail()
        {
            try
            {
                _logger.LogInformation("Fetching sales detail data.");

                var salesSummary = _salesDataService.GetSalesDetail();

                if (salesSummary == null || !salesSummary.Any())
                {
                    _logger.LogWarning("No sales detail data found.");
                    return SalesApiResponse<IEnumerable<SalesDto>>.Success([], "No sales detail data available");

                }

                _logger.LogInformation("Successfully retrieved {SummaryCount} sales detail records.", salesSummary.Count());
                return SalesApiResponse<IEnumerable<SalesDto>>.Success(salesSummary, "Successfully Reterived Data ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching sales summary data.");

                return StatusCode(StatusCodes.Status500InternalServerError, SalesApiResponse<IEnumerable<SalesDto>>.Fail("An error occurred while processing your request", [ex.Message]));

            }
        }

        [HttpGet("summary", Name = nameof(GetSalesSummaryData))]
        public ActionResult<SalesApiResponse<IEnumerable<SalesSummaryDto>>> GetSalesSummaryData()
        {
            try
            {
                _logger.LogInformation("Fetching sales summary data.");

                var salesSummary = _salesDataService.GetSalesSummary();

                if (salesSummary == null || !salesSummary.Any())
                {
                    _logger.LogWarning("No sales summary data found.");
                    return SalesApiResponse<IEnumerable<SalesSummaryDto>>.Success([], "No sales data available");
                       
                }

                _logger.LogInformation("Successfully retrieved {SummaryCount} sales summary records.", salesSummary.Count());
                return SalesApiResponse<IEnumerable<SalesSummaryDto>>.Success(salesSummary, "Successfully Reterived Data ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching sales summary data.");

                return StatusCode(StatusCodes.Status500InternalServerError,SalesApiResponse<IEnumerable<SalesSummaryDto>>.Fail("An error occurred while processing your request", [ex.Message] ));
               
            }
        }
    }
}
