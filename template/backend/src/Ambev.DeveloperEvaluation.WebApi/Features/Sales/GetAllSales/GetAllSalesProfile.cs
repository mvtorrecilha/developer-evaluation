using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetAllSalesProfile : Profile
{
    public GetAllSalesProfile()
    {

        CreateMap<GetAllSalesSaleItemResult, GetSaleItemResponse>();

        CreateMap<GetAllSalesResult, GetSaleResponse>()
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(src => src.Cancelled));
    }
}
