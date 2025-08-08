namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
public class DeleteSaleResult
{
    public bool Success { get; set; }

    public static DeleteSaleResult SuccessResult() => new() { Success = true };
    public static DeleteSaleResult FailureResult() => new() { Success = false };
}
