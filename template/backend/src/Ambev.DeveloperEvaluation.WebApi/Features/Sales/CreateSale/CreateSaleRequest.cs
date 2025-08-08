namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public Guid BranchId { get; set; }
    public string BranchName { get; set; }
    public List<CreateSaleCartItemRequest> CartItems { get; set; }

    public CreateSaleRequest()
    {
        CartItems = new List<CreateSaleCartItemRequest>();
    }

    public CreateSaleRequest(Guid customerId, string customerName, Guid branchId, string branchName, List<CreateSaleCartItemRequest> cartItems)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        CartItems = cartItems ?? new List<CreateSaleCartItemRequest>();
    }
}