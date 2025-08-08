using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetAllSalesProfile : Profile
{
    public GetAllSalesProfile()
    {
        CreateMap<SaleItem, GetAllSalesSaleItemResult>();

        CreateMap<Sale, GetAllSalesResult>();

        CreateMap<Sale, GetAllSalesResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalAmount));

    }
}
