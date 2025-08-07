namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleResult
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public DateTime SaleDate { get; set; }

    public List<GetSaleItemResult> SaleItems { get; set; } = [];

    public decimal TotalSaleAmount { get; set; }

    public bool IsCanceled { get; set; }
}
