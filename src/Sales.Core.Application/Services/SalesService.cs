using AutoMapper;
using Microsoft.Extensions.Options;
using Sales.Core.Application.DTO_Entities;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain;
using Sales.Core.Domain.Model;

namespace Sales.Core.Application.Services
{
    public class SalesService(ISalesRepository salesDataRepository, IOptions<FileConfiguration> fileSettings, IMapper mapper) : ISalesService
    {
        private readonly ISalesRepository _salesDataRepository = salesDataRepository;
        private readonly FileConfiguration _fileSettings = fileSettings.Value ?? throw new ArgumentNullException(nameof(fileSettings));
        private readonly IMapper _mapper = mapper;

        private List<SalesDto> GetSaleDataDto()
        {
            var salesData = _salesDataRepository.GetSalesData(_fileSettings.SalesDataFilePath);
            var SalesDto = _mapper.Map<List<SalesDto>>(salesData);
            return SalesDto;
        }

        public IEnumerable<SalesSummaryDto> GetSalesSummary()
        {
            var groupedSales = GetSaleDataDto()

                .GroupBy(s => new { s.Country, s.Segment })
                .Select(g => new SalesSummaryDto
                {
                    Country = g.Key.Country,
                    Segment = g.Key.Segment,
                    TotalUnitsSold = g.Sum(s => s.UnitsSold),

                    TotalManufacturingPrice = g.Sum(s => s.ManufacturingPrice),
                    TotalSalePrice = g.Sum(s => s.SalePrice)
                })
                  .OrderBy(s => s.Country)
                  .ThenBy(s => s.Segment)
                .ToList();

            return groupedSales;
        }

        public IEnumerable<SalesDto> GetSalesDetail()
        {
            return GetSaleDataDto();
        }
    }
}
