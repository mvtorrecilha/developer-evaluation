namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
public class CreateSaleItemResponse
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount { get; set; }

    public CreateSaleItemResponse() { }

    public CreateSaleItemResponse(Guid productId, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        TotalAmount = totalAmount;
    }
}
