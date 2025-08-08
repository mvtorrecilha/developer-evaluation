using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;

public record CancelItemSaleCommand(Guid SaleId, Guid ItemId) : IRequest<CancelItemSaleResult>;
