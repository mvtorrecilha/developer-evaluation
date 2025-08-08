namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime SaleDate { get; set; }
    public List<CreateSaleItemResponse> SaleItems { get; set; }
    public decimal TotalSaleAmount { get; set; }
    public decimal TotalSaleDiscount { get; set; }
    public string Branch { get; set; }
    public bool IsCanceled { get; set; }

    public CreateSaleResponse()
    {
        SaleItems = new List<CreateSaleItemResponse>();
    }

    public CreateSaleResponse(Guid id, Guid userId, DateTime saleDate, List<CreateSaleItemResponse> saleItems, decimal totalSaleAmount, decimal totalSaleDiscount, string branch, bool isCanceled)
    {
        Id = id;
        UserId = userId;
        SaleDate = saleDate;
        SaleItems = saleItems ?? new List<CreateSaleItemResponse>();
        TotalSaleAmount = totalSaleAmount;
        TotalSaleDiscount = totalSaleDiscount;
        Branch = branch;
        IsCanceled = isCanceled;
    }
}