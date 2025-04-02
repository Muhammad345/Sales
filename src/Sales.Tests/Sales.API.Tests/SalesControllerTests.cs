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

        [Fact]
        public void GetSalesDetail_ReturnsSuccess_WithData()
        {
            // Arrange
            var salesData = new List<SalesDto>
            {
                new() { Country = "USA", Segment = "Retail", UnitsSold = 100 },
                new() { Country = "UK", Segment = "Wholesale", UnitsSold = 200 }
            };
            _mockSalesService.Setup(x => x.GetSalesDetail()).Returns(salesData);

            // Act
            var result = _controller.GetSalesDetail();

            // Assert
            var okResult = Assert.IsType<ActionResult<SalesApiResponse<IEnumerable<SalesDto>>>>(result);
            var response = Assert.IsType<SalesApiResponse<IEnumerable<SalesDto>>>(okResult.Value);

            Assert.True(response.IsSuccessful);
            Assert.Equal("Successfully Reterived Data ", response.Message);
            Assert.Equal(salesData, response.Data);
            Assert.Equal(2, response.Data.Count());

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Fetching sales detail data")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Successfully retrieved 2 sales detail records")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public void GetSalesDetail_ReturnsSuccess_WithNoData()
        {
            // Arrange
            _mockSalesService.Setup(x => x.GetSalesDetail()).Returns(new List<SalesDto>());

            // Act
            var result = _controller.GetSalesDetail();

            // Assert
            var okResult = Assert.IsType<ActionResult<SalesApiResponse<IEnumerable<SalesDto>>>>(result);
            var response = Assert.IsType<SalesApiResponse<IEnumerable<SalesDto>>>(okResult.Value);

            Assert.True(response.IsSuccessful);
            Assert.Equal("No sales detail data available", response.Message);
            Assert.Empty(response.Data);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No sales detail data found")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }

        [Fact]
        public void GetSalesDetail_ReturnsError_WhenExceptionThrown()
        {
            // Arrange
            var exception = new Exception("Database connection failed");
            _mockSalesService.Setup(x => x.GetSalesDetail()).Throws(exception);

            // Act
            var result = _controller.GetSalesDetail();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);

            var response = Assert.IsType<SalesApiResponse<IEnumerable<SalesDto>>>(statusCodeResult.Value);
            Assert.False(response.IsSuccessful);
            Assert.Equal("An error occurred while processing your request", response.Message);
            Assert.Contains("Database connection failed", response.Errors);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("An error occurred while fetching sales summary data")),
                    exception,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once());
        }
    }
}
