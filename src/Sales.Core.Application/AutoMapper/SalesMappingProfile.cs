namespace Sales.Core.Application.AutoMapper
{
    using global::AutoMapper;
    using Sales.Core.Application.DTO_Entities;
    using Sales.Core.Application.Utility;
    using Sales.Core.Domain.Model;

    public class SalesMappingProfile : Profile
    {
        public SalesMappingProfile()
        {
            CreateMap<SalesData, SalesDto>()
                .ForMember(dest => dest.UnitsSold, opt => opt.MapFrom(src => CurrencyParser.GetAmount(src.UnitsSold)))
                .ForMember(dest => dest.ManufacturingPrice, opt => opt.MapFrom(src => CurrencyParser.GetAmount(src.ManufacturingPrice)))
                .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => CurrencyParser.GetAmount(src.SalePrice)))
                .ForMember(dest => dest.UnitsSoldCurrency, opt => opt.MapFrom(src => CurrencyParser.GetCurrencySymbol(src.SalePrice)))
                .ForMember(dest => dest.ManufacturingPriceCurrency, opt => opt.MapFrom(src => CurrencyParser.GetCurrencySymbol(src.ManufacturingPrice)))
                .ForMember(dest => dest.SalePriceCurrency, opt => opt.MapFrom(src => CurrencyParser.GetCurrencySymbol(src.SalePrice)));
        }
    }

}
