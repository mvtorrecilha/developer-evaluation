using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;

public class CancelItemSaleCommandValidator : AbstractValidator<CancelItemSaleCommand>
{
    public CancelItemSaleCommandValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty().WithMessage("SaleId must not be empty.");
        RuleFor(x => x.ItemId).NotEmpty().WithMessage("ItemId must not be empty.");
    }
}