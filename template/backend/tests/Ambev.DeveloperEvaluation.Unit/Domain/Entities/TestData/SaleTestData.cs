using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Gera instâncias de Sale e SaleItem com dados válidos e aleatórios para testes unitários.
/// </summary>
public static class SaleTestData
{
    private static readonly Faker Faker = new();

    /// <summary>
    /// Gera uma instância de Sale válida com dados aleatórios, mas sem itens.
    /// Use AddItem separadamente para adicionar produtos.
    /// </summary>
    public static Sale GenerateBasicSale()
    {
        return new Sale(
            customerId: Guid.NewGuid(),
            customerName: Faker.Name.FullName(),
            branchId: Guid.NewGuid(),
            branchName: Faker.Company.CompanyName()
        );
    }

    /// <summary>
    /// Gera uma instância de SaleItem com quantidade válida (<= 20) e desconto calculado.
    /// </summary>
    public static SaleItem GenerateValidSaleItem()
    {
        var quantity = Faker.Random.Int(1, 20);
        var unitPrice = Faker.Random.Decimal(5, 100);

        return new SaleItem(
            productId: Guid.NewGuid(),
            productName: Faker.Commerce.ProductName(),
            quantity: quantity,
            unitPrice: unitPrice
        );
    }

    /// <summary>
    /// Gera uma lista de itens de venda válidos.
    /// </summary>
    public static List<SaleItem> GenerateValidSaleItems(int count = 3)
    {
        return Enumerable.Range(0, count)
            .Select(_ => GenerateValidSaleItem())
            .ToList();
    }

    /// <summary>
    /// Gera uma Sale completa com itens válidos e TotalAmount calculado.
    /// </summary>
    public static Sale GenerateCompleteSale(int itemCount = 3)
    {
        var sale = GenerateBasicSale();

        foreach (var item in GenerateValidSaleItems(itemCount))
        {
            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        }

        return sale;
    }

    /// <summary>
    /// Gera uma Sale com item inválido (quantidade > 20) para testar exceção.
    /// </summary>
    public static SaleItem GenerateInvalidSaleItem_QuantityAboveLimit()
    {
        return new SaleItem(
            productId: Guid.NewGuid(),
            productName: Faker.Commerce.ProductName(),
            quantity: 21,
            unitPrice: Faker.Random.Decimal(5, 100)
        );
    }

    /// <summary>
    /// Gera dados para UpdateItems.
    /// </summary>
    public static List<(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice)> GenerateUpdatedItemsList()
    {
        return Enumerable.Range(0, 2).Select(_ => (
            ProductId: Guid.NewGuid(),
            ProductName: Faker.Commerce.ProductName(),
            Quantity: Faker.Random.Int(1, 10),
            UnitPrice: Faker.Random.Decimal(10, 200)
        )).ToList();
    }
}