using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles retrieving a sale by its identifier.
    /// </summary>
    /// <param name="request">The query containing the sale ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result containing the sale details.</returns>
    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"Sale with Id {request.Id} not found.");

        return _mapper.Map<GetSaleResult>(sale);
    }
}
