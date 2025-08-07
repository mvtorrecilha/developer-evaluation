using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<DeleteSaleResult, DeleteSaleResponse>()
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success));
    }
}
