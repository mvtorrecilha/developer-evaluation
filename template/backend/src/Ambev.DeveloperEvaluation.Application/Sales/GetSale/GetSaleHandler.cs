using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public GetSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<GetSaleHandler> logger
        )
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles retrieving a sale by its identifier.
    /// </summary>
    /// <param name="request">The query containing the sale ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result containing the sale details.</returns>
    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {GetSaleHandler} triggered for SaleId {SaleId}", nameof(UpdateSaleHandler), request.Id);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with Id {request.Id} not found.");

        _logger.LogInformation("Sale retrieved for {Id}", request.Id);
        return _mapper.Map<GetSaleResult>(sale);
    }
}
