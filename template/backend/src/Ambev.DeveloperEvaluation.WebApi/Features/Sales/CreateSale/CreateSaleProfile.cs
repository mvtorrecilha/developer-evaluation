using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCartItemRequest, CreateSaleCartItem>()
            .ConstructUsing(src => new CreateSaleCartItem(
                src.ProductId,
                src.ProductName ?? string.Empty,
                src.Quantity,
                src.UnitPrice
            ));

        CreateMap<CreateSaleRequest, CreateSaleCommand>();

        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.BranchName))
            .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.TotalSaleDiscount, opt => opt.MapFrom(src => src.Items.Sum(x => x.Discount)))
            .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.IsCanceled, opt => opt.MapFrom(src => src.Cancelled));

        CreateMap<CreateSaleResult, CreateSaleResponse>().ReverseMap();
    }
}