namespace Sales.Core.Application.DTO_Entities
{
    public class SalesDto
    {
        public string Segment { get; set; } = ApplicationCoreConstants.DefaultSegment;
        public string Country { get; set; } = ApplicationCoreConstants.DefaultCountry;
        public string Product { get; set; } = ApplicationCoreConstants.DefaultProduct;
        public string DiscountBand { get; set; } = ApplicationCoreConstants.DefaultDiscountBand;
        public string UnitsSoldCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
        public decimal UnitsSold { get; set; }
        public string ManufacturingPriceCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
        public decimal ManufacturingPrice { get; set; }
        public string SalePriceCurrency { get; set; } = ApplicationCoreConstants.DefaultCurrency;
        public decimal SalePrice { get; set; }
        public DateTime Date { get; set; }
    }
}
