using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItemSale;
public class CancelItemSaleProfile : Profile
{
    public CancelItemSaleProfile()
    {
        CreateMap<CancelItemSaleRequest, CancelItemSaleCommand>()
            .ConstructUsing(src => new CancelItemSaleCommand(src.SaleId, src.ItemId));
    }
}