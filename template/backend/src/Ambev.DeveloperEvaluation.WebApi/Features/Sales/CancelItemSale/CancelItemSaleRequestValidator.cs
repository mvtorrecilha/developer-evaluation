﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelItemSale;

public class CancelItemSaleRequestValidator : AbstractValidator<CancelItemSaleRequest>
{
    public CancelItemSaleRequestValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
        RuleFor(x => x.ItemId).NotEmpty();
    }
}
