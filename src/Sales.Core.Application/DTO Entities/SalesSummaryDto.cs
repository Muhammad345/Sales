namespace Sales.Core.Application.DTO_Entities
{
    public class SalesSummaryDto
    {
        public string Country { get; set; } = ApplicationCoreConstants.DefaultCountry;
        public string Segment { get; set; } = ApplicationCoreConstants.DefaultSegment;
        public decimal TotalUnitsSold { get; set; }
        public string UnitsSoldCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
        public decimal TotalManufacturingPrice { get; set; }
        public string ManufacturingPriceCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
        public decimal TotalSalePrice { get; set; }
        public string SalePriceCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
    }
}
