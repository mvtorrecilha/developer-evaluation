namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public record UpdateSaleResponse(
    Guid Id,
    Guid UserId,
    DateTime SaleDate,
    List<UpdateSaleItemResponse> SaleItems,
    decimal TotalSaleAmount,
    decimal TotalSaleDiscount,
    string Branch,
    bool IsCanceled
)
{
    public UpdateSaleResponse() : this(Guid.Empty, Guid.Empty, DateTime.MinValue, [], 0m, 0m, string.Empty, false) { }
}