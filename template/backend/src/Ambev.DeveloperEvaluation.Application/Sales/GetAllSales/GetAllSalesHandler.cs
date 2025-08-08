using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Handles the get all sales.
/// </summary>
public class GetAllSalesHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<GetAllSalesResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public GetAllSalesHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<GetAllSalesHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles retrieving all sales.
    /// </summary>
    /// <param name="request">The query to get all sales.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of sales results.</returns>
    public async Task<IEnumerable<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all sales");

        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GetAllSalesResult>>(sales);
    }
}
