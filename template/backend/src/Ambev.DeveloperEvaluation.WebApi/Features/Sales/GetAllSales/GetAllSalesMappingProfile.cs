using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetAllSalesMappingProfile : Profile
{
    public GetAllSalesMappingProfile()
    {
        CreateMap<GetAllSalesRequest, GetAllSalesQuery>();

        CreateMap<Sale, GetSaleResponse>()
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items));

        CreateMap<SaleItem, GetSaleItemResponse>();
    }
}
