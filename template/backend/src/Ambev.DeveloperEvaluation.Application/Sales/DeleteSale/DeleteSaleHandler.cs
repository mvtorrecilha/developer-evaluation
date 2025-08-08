using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handles the delete of a sale.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<DeleteSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public DeleteSaleHandler(
        ISaleRepository saleRepository,
        ILogger<DeleteSaleHandler> logger
        )
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    // <summary>
    /// Handles deletion of a sale by its identifier.
    /// </summary>
    /// <param name="request">The command containing the sale ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result indicating the success of the deletion.</returns>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {DeleteSaleHandler} triggered for SaleId {SaleId}", nameof(UpdateSaleHandler), command.Id);

        var sale = await _saleRepository.GetByIdAsync(command.Id);
        if (sale == null)
            return DeleteSaleResult.FailureResult();

        await _saleRepository.DeleteAsync(sale, cancellationToken);

        _logger.LogInformation("Sale deleted successfully. {SaleId}", command.Id);

        return DeleteSaleResult.SuccessResult();
    }
}