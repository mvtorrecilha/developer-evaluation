using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for the UpdateSaleCommand to ensure all required fields are valid.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Sale ID is required.");
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name max length is 100.");
        RuleFor(x => x.BranchId).NotEmpty().WithMessage("Branch ID is required.");
        RuleFor(x => x.BranchName)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(100).WithMessage("Branch name max length is 100.");
        RuleFor(x => x.CartItems)
            .NotEmpty().WithMessage("At least one sale item is required.");

        RuleForEach(x => x.CartItems).SetValidator(new UpdateSaleItemValidator());
    }
}
