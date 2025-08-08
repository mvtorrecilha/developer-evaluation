using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

public static class GetAllSalesHandlerTestsData
{
    public static List<Sale> GenerateValidSales(int count = 3)
    {
        var faker = new Faker();
        var sales = new List<Sale>();

        for (int i = 0; i < count; i++)
        {
            var sale = new Sale(
                Guid.NewGuid(),
                faker.Person.FullName,
                Guid.NewGuid(),
                faker.Company.CompanyName()
            );

            sale.AddItem(Guid.NewGuid(), "Product A", 2, 10.5m);
            sale.AddItem(Guid.NewGuid(), "Product B", 1, 20.0m);

            sales.Add(sale);
        }

        return sales;
    }

    public static IEnumerable<GetAllSalesResult> MapToResult(IEnumerable<Sale> sales)
    {
        return sales.Select(s => new GetAllSalesResult
        {
            Id = s.Id,
            CustomerId = s.CustomerId,
            CustomerName = s.CustomerName,
            BranchId = s.BranchId,
            BranchName = s.BranchName,
            Date = s.Date,
            Total = s.TotalAmount,
            Cancelled = s.Cancelled
        });
    }
}
