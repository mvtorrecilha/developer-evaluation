using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime SaleDate { get; set; }
    public List<UpdateSaleItem> SaleItems { get; set; } = new();
    public decimal TotalSaleAmount { get; set; }
    public decimal TotalSaleDiscount { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsCanceled { get; set; }

    public static UpdateSaleResult FromSale(Sale sale)
    {
        return new UpdateSaleResult
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            SaleDate = sale.Date,
            SaleItems = sale.Items.Select(item => new UpdateSaleItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList(),
            TotalSaleAmount = sale.TotalAmount,
            TotalSaleDiscount = sale.Items.Sum(i => i.Discount),
            BranchName = sale.BranchName,
            IsCanceled = sale.Cancelled
        };
    }
}