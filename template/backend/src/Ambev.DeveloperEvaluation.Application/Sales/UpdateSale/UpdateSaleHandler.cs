using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;

        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handler {UpdateSaleHandler} triggered for SaleId {SaleId}", nameof(UpdateSaleHandler), command.Id);

        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning($"Validation failed for {nameof(UpdateSaleCommand)}");

            throw new ValidationException(validationResult.Errors);
        }

        var sale = await _saleRepository.GetByIdAsync(command.Id);
        if (sale is null)
            throw new KeyNotFoundException($"Sale with Id {command.Id} not found.");


        var updatedItems = command.CartItems
            .Select(i => (i.ProductId, i.ProductName, i.Quantity, i.UnitPrice))
            .ToList();

        sale.BranchId = command.BranchId;
        sale.BranchName = command.BranchName;
        sale.CustomerId = command.CustomerId;
        sale.CustomerName = command.CustomerName;

        sale.UpdateItems(updatedItems);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        _logger.LogInformation("Sale successfully updated with Id: {SaleId}", sale.Id);

        //TODO: Publish SaleUpdatedEvent
        _logger.LogInformation("SaleUpdatedEvent published successfully for SaleId: {SaleId}", sale.Id);

        var result = _mapper.Map<UpdateSaleResult>(sale);
        return result;
    }

}
