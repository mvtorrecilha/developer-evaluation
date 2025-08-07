using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handles the delete of a sale.
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public DeleteSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    // <summary>
    /// Handles deletion of a sale by its identifier.
    /// </summary>
    /// <param name="request">The command containing the sale ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result indicating the success of the deletion.</returns>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id);
        if (sale == null)
            return DeleteSaleResult.FailureResult();

        await _saleRepository.DeleteAsync(sale, cancellationToken);

        return DeleteSaleResult.SuccessResult();
    }
}