using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class GetSaleHandlerTestsData
{
    public static Sale GenerateValidSale()
    {
        var faker = new Faker();

        var sale = new Sale(
            faker.Random.Guid(),                
            faker.Person.FullName,              
            faker.Random.Guid(),                
            faker.Company.CompanyName()         
        );

        sale.AddItem(Guid.NewGuid(), "Product A", 3, 10.50m);
        sale.AddItem(Guid.NewGuid(), "Product B", 1, 20.00m);

        return sale;
    }

    public static GetSaleQuery GenerateValidQuery(Guid saleId)
    {
        return new GetSaleQuery(saleId);
    }

    public static List<GetSaleItemResult> GenerateSaleItemResults()
    {
        return
        [
            new GetSaleItemResult
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product A",
                Quantity = 2,
                UnitPrice = 15.5m
            },
            new GetSaleItemResult
            {
                ProductId = Guid.NewGuid(),
                ProductName = "Product B",
                Quantity = 1,
                UnitPrice = 25m
            }
        ];
    }
}