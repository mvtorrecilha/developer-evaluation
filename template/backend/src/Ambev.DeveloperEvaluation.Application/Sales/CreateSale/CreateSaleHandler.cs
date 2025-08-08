using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handles the creation of a sale.
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository to access sales data.</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the creation of a new sale.
    /// </summary>
    /// <param name="command">The command containing sale creation details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the sale creation.</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {Handler} started with CustomerId: {CustomerId}", nameof(CreateSaleHandler), command.CustomerId);

        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for {Command}", nameof(CreateSaleCommand));
            throw new ValidationException(validationResult.Errors);
        }

        if (command.CartItems == null || command.CartItems.Count == 0)
        {
            _logger.LogWarning("Cart is empty. Cannot proceed with sale.");
            throw new ArgumentException("The cart must contain at least one item.");
        }

        var sale = new Sale(
            command.CustomerId,
            command.CustomerName,
            command.BranchId,
            command.BranchName
        );

        foreach (var item in command.CartItems)
        {
            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        }

        var saleCreated = await _saleRepository.CreateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale successfully created with Id: {SaleId}", saleCreated.Id);

        //TODO: Publish SaleCreatedEvent
        _logger.LogInformation("SaleCreatedEvent published successfully for SaleId: {SaleId}", saleCreated.Id);

        var result = _mapper.Map<CreateSaleResult>(saleCreated);
        return result;
    }
}
