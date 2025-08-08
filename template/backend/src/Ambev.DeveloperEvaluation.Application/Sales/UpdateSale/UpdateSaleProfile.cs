using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<Sale, UpdateSaleResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalSaleDiscount, opt => opt.MapFrom(src => src.Items.Sum(i => i.Discount)))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(src => src.Cancelled));

        CreateMap<SaleItem, UpdateSaleItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
    }
}