namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<UpdateSaleItemRequest> CartItems { get; set; } = [];

    public UpdateSaleRequest() { }

    public UpdateSaleRequest(Guid customerId, string customerName, Guid branchId, string branchName, List<UpdateSaleItemRequest> cartItems)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        CartItems = cartItems;
    }
}