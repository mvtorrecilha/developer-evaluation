using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public record CreateSaleResult(
    Guid Id = default,
    Guid CustomerId = default,
    DateTime SaleDate = default,
    List<SaleItem>? SaleItems = null,
    decimal TotalSaleAmount = 0,
    decimal TotalSaleDiscount = 0,
    string Branch = "",
    bool IsCanceled = false)
{
    public CreateSaleResult() : this(default, default, default, [], 0, 0, "", false) { }

    public static CreateSaleResult FromSale(Sale sale)
    {
        return new CreateSaleResult(
            sale.Id,
            sale.CustomerId,
            sale.Date,
            sale.Items,
            sale.TotalAmount,
            sale.Items.Sum(item => item.Discount),
            sale.BranchName,
            sale.Cancelled
        );
    }
}