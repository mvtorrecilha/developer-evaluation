using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public List<UpdateSaleItem> CartItems { get; set; } = [];

    public UpdateSaleCommand() { }

    public UpdateSaleCommand(Guid id, Guid customerId, string customerName, Guid branchId, string branchName, List<UpdateSaleItem> cartItems)
    {
        Id = id;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        CartItems = cartItems;
    }
}