namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSales;

public class GetSaleItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
