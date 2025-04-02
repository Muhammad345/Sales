using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sales.API.Controllers;
using Sales.Core.Application.DTO_Entities;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain;

namespace Sales.Tests.Sales.API.Tests
{
    public class SalesControllerTests
    {
        private readonly Mock<ILogger<SalesController>> _mockLogger;
        private readonly Mock<ISalesService> _mockSalesService;
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            _mockLogger = new Mock<ILogger<SalesController>>();
            _mockSalesService = new Mock<ISalesService>();
            _controller = new SalesController(_mockLogger.Object, _mockSalesService.Object);
        }

        [Fact]
        public void GetSalesSummaryData_ReturnsSuccess_WhenSalesSummaryIsFound()
        {
            // Arrange
            var mockSalesSummary = TestDataGenerator.GenerateSalesSummaryDtos(2);

            _mockSalesService.Setup(service => service.GetSalesSummary()).Returns(mockSalesSummary);

            // Act
            var salesApiresponse = _controller.GetSalesSummaryData();

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesApiResponse<IEnumerable<SalesSummaryDto>>>>(salesApiresponse);

            Assert.NotNull(salesApiresponse.Value);
        }

        [Fact]
        public void GetSalesSummaryData_ReturnsNoContent_WhenNoSalesSummaryData()
        {
            // Arrange
            _mockSalesService.Setup(service => service.GetSalesSummary()).Returns([]);

            // Act
            var result = _controller.GetSalesSummaryData();

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesApiResponse<IEnumerable<SalesSummaryDto>>>>(result);
        }

        [Fact]
        public void GetSalesSummaryData_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockSalesService.Setup(service => service.GetSalesSummary()).Throws(new System.Exception("Some error"));

            // Act
            var result = _controller.GetSalesSummaryData();

            // Assert
            var actionResult = Assert.IsType<ActionResult<SalesApiResponse<IEnumerable<SalesSummaryDto>>>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            var response = Assert.IsType<SalesApiResponse<IEnumerable<SalesSummaryDto>>>(statusCodeResult.Value);
            Assert.False(response.IsSuccessful);
            Assert.Equal("An error occurred while processing your request", response.Message);
        }
    }
}
