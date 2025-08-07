namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public record GetAllSalesResult
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public Guid BranchId { get; set; }

    public string BranchName { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public decimal Total { get; set; }

    public List<GetAllSalesSaleItemResult> SaleItems { get; set; } = [];

    public bool Cancelled { get; set; }
}