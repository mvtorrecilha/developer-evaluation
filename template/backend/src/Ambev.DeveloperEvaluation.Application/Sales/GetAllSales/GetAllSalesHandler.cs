using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<GetAllSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;

    public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetAllSalesHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all sales");

        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GetAllSalesResult>>(sales);
    }
}
