using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handles the cancellation of a sale.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public CancelSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the cancellation of a sale by its ID.
    /// </summary>
    /// <param name="request">The command containing the sale ID to cancel.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A result indicating the success of the cancellation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the sale is not found.</exception>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with Id {request.Id} not found.");

        sale.CancelSale();
            
        await _saleRepository.UpdateAsync(sale);

        return new CancelSaleResult
        {
            Id = sale.Id,
            Success = true
        };
    }
}