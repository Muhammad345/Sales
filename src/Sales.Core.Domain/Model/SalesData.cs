namespace Sales.Core.Domain.Model
{
    public class SalesData
    {
        public string Segment { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string DiscountBand { get; set; } = string.Empty;
        public string UnitsSold { get; set; } = string.Empty;
        public string ManufacturingPrice { get; set; } = string.Empty;
        public string SalePrice { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
