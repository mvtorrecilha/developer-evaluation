using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class DeleteSaleHandlerTestsData
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

        sale.AddItem(Guid.NewGuid(), "Product A", 1, 100);
        sale.AddItem(Guid.NewGuid(), "Product B", 2, 50);

        return sale;
    }

    public static Guid GenerateRandomSaleId()
    {
        return Guid.NewGuid();
    }
}