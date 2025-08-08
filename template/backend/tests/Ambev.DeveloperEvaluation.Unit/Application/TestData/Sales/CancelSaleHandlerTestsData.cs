using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class CancelSaleHandlerTestsData
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

        sale.AddItem(Guid.NewGuid(), "Product A", 2, 10.5m);
        sale.AddItem(Guid.NewGuid(), "Product B", 1, 20.0m);

        return sale;
    }

    public static CancelSaleCommand GenerateValidCommand(Guid saleId)
    {
        return new CancelSaleCommand(saleId);
    }
}
