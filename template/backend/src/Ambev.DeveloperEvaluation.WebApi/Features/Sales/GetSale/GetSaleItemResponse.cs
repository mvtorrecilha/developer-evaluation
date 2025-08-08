﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}