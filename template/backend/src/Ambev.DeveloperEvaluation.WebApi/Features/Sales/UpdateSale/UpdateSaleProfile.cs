using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleItemRequest, UpdateSaleItem>();

        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems));

        CreateMap<Sale, UpdateSaleResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalSaleDiscount, opt => opt.MapFrom(src => src.Items.Sum(x => x.Discount)))
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(src => src.Cancelled));

        CreateMap<UpdateSaleResult, UpdateSaleResponse>().ReverseMap();
        CreateMap<UpdateSaleItem, UpdateSaleItemResponse>();
    }
}