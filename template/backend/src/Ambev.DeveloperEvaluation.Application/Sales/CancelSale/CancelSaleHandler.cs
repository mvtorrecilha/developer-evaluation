using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;

    public CancelSaleHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

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