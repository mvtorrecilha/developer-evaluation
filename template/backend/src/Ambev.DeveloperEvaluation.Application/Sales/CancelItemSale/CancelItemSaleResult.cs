namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;

public class CancelItemSaleResult
{
    public Guid SaleId { get; set; }
    public Guid ItemId { get; set; }
    public bool Success { get; set; }
}
