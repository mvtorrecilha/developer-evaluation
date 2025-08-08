using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    private static readonly Faker Faker = new();

    public static Faker<SaleItem> Generate()
    {
        return new Faker<SaleItem>()
            .CustomInstantiator(f => new SaleItem(
                productId: f.Random.Guid(),
                productName: f.Commerce.ProductName(),
                quantity: f.Random.Int(1, 20),
                unitPrice: f.Random.Decimal(1, 1000)
            ));
    }

    public static SaleItem Valid() => Generate().Generate();

    public static List<SaleItem> ValidList(int count = 3) => Generate().Generate(count);

    public static SaleItem GenerateValidSaleItem(int quantity, decimal unitPrice)
    {
        return new SaleItem(
            productId: Guid.NewGuid(),
            productName: Faker.Commerce.ProductName(),
            quantity: quantity,
            unitPrice: unitPrice
        );
    }

    public static SaleItem Generate(int quantity, decimal unitPrice)
    {
        return new SaleItem(
            productId: Guid.NewGuid(),
            productName: Faker.Commerce.ProductName(),
            quantity: quantity,
            unitPrice: unitPrice
        );
    }
}