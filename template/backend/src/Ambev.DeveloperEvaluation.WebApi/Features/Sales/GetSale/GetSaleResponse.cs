namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
public class GetSaleResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public List<GetSaleItemResponse> SaleItems { get; set; } = [];
    public decimal TotalSaleAmount { get; set; }
    public bool IsCanceled { get; set; }
}
