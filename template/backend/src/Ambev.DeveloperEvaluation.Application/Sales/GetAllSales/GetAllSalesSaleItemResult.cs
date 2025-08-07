namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public record GetAllSalesSaleItemResult
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}