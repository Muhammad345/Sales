using Sales.Core.Application.DTO_Entities;

namespace Sales.Tests
{
    public static class TestDataGenerator
    {

        private static readonly Random _random = new Random();

        public static SalesSummaryDto GenerateSalesSummaryDto()
        {
            return new SalesSummaryDto
            {
                Country = "USA",  
                Segment = "Electronics",  
                TotalUnitsSold = GenerateRandomDecimal(1, 1000),
                UnitsSoldCurrency = "USD", 
                TotalManufacturingPrice = GenerateRandomDecimal(5000, 50000),
                ManufacturingPriceCurrency = "USD", 
                TotalSalePrice = GenerateRandomDecimal(10000, 100000),
                SalePriceCurrency = "USD"
            };
        }

        public static List<SalesSummaryDto> GenerateSalesSummaryDtos(int count)
        {
            var salesSummaryDtos = new List<SalesSummaryDto>();

            for (int i = 0; i < count; i++)
            {
                salesSummaryDtos.Add(GenerateSalesSummaryDto());
            }

            return salesSummaryDtos;
        }

        private static decimal GenerateRandomDecimal(decimal min, decimal max)
        {
            double value = _random.NextDouble() * (double)(max - min) + (double)min;
            return Math.Round((decimal)value, 2);  // Round to 2 decimal places
        }
    }
}
