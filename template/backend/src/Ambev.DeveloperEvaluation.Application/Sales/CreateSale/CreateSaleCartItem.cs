namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public record CreateSaleCartItem(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice);