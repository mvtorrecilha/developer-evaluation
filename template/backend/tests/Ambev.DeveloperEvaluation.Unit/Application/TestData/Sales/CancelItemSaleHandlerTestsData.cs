using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;


public static class CancelItemSaleHandlerTestsData
{
    public static Sale GenerateValidSaleWithItems()
    {
        var faker = new Faker();

        var sale = new Sale(
            customerId: faker.Random.Guid(),
            customerName: faker.Person.FullName,
            branchId: faker.Random.Guid(),
            branchName: faker.Company.CompanyName()
        );
        sale.Id = faker.Random.Guid();

        var item1Id = faker.Random.Guid();
        var item2Id = faker.Random.Guid();

        var item1 = new SaleItem(item1Id, "Product A", 2, 10.5m);
        var item2 = new SaleItem(item2Id, "Product B", 1, 20.0m);

        item1.ProductId = faker.Random.Guid();
        item2.ProductId = faker.Random.Guid();

        sale.Items.Add(item1);
        sale.Items.Add(item2);

        return sale;
    }

    public static CancelItemSaleCommand GenerateValidCommand(Sale sale)
    {
        var item = sale.Items.First();
        return new CancelItemSaleCommand(sale.Id, item.ProductId);
    }

    public static CancelItemSaleCommand GenerateCommandWithInvalidItemId(Sale sale)
    {
        Guid invalidItemId;
        do
        {
            invalidItemId = Guid.NewGuid();
        } while (sale.Items.Any(i => i.Id == invalidItemId));

        return new CancelItemSaleCommand(sale.Id, invalidItemId);
    }

    public static CancelItemSaleCommand GenerateCommandWithInvalidSaleId()
    {
        return new CancelItemSaleCommand(Guid.NewGuid(), Guid.NewGuid());
    }
}