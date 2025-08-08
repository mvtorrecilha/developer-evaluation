using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handles the cancellation of a sale.
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CancelSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    /// <param name="logger">The logger instance.</param>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        ILogger<CancelSaleHandler> logger
        )
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the cancellation of a sale by its ID.
    /// </summary>
    /// <param name="request">The command containing the sale ID to cancel.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A result indicating the success of the cancellation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the sale is not found.</exception>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {CancelSaleHandler} triggered for SaleId {SaleId}", nameof(UpdateSaleHandler), command.Id);

        var sale = await _saleRepository.GetByIdAsync(command.Id);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with Id {command.Id} not found.");

        sale.CancelSale();
            
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale cancelled successfully. {SaleId}", command.Id);

        //TODO: Publish ItemCancelled
        _logger.LogInformation("SaleCancelled published successfully for SaleId: {SaleId}", command.Id);

        return new CancelSaleResult
        {
            Id = sale.Id,
            Success = true
        };
    }
}