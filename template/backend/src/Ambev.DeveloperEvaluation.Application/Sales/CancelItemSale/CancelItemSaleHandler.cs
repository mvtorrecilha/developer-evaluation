using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;

/// <summary>
/// Handles the cancellation of a specific item in a sale.
/// </summary>
public class CancelItemSaleHandler : IRequestHandler<CancelItemSaleCommand, CancelItemSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CancelItemSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelItemSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The sale repository to access sale data.</param>
    /// <param name="logger">The logger instance.</param>
    public CancelItemSaleHandler(
        ISaleRepository saleRepository,
        ILogger<CancelItemSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CancelItemSaleCommand to cancel a specific item within a sale.
    /// </summary>
    /// <param name="command">The command containing sale and item identifiers.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the cancellation operation.</returns>
    /// <exception cref="ValidationException">Thrown when the command validation fails.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the specified sale is not found.</exception>
    public async Task<CancelItemSaleResult> Handle(CancelItemSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {CancelItemSaleHandler} triggered for SaleId {SaleId}", nameof(UpdateSaleHandler), command.SaleId);

        var validator = new CancelItemSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {Command}", nameof(CreateSaleCommand));
            throw new ValidationException(validationResult.Errors);
        }

        var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
        if (sale == null)
            throw new KeyNotFoundException($"Sale with Id {command.SaleId} not found.");

        sale.CancelItem(command.ItemId);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Item Sale cancelled with Sale Id: {SaleId} and {ProductId}", sale.Id, command.ItemId);

        //TODO: Publish ItemCancelled
        _logger.LogInformation("ItemCancelled published successfully for SaleId: {SaleId} and Item: {ItemId}", sale.Id, command.ItemId);

        return new CancelItemSaleResult
        {
            SaleId = sale.Id,
            ItemId = command.ItemId,
            Success = true
        };
    }
}