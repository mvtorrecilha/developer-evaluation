using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;


public record GetAllSalesQuery : IRequest<IEnumerable<GetAllSalesResult>>;