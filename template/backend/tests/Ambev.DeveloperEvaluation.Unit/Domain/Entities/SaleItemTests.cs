using Ambev.DeveloperEvaluation.Domain.Entities;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Theory(DisplayName = "Should create SaleItem with valid parameters")]
    [InlineData(1, 10)]
    [InlineData(4, 50)]
    [InlineData(10, 100)]
    [InlineData(20, 200)]
    public void SaleItem_Creation_ShouldSucceed_ForValidQuantities(int quantity, decimal unitPrice)
    {
        // Arrange & Act
        var item = new SaleItem(Guid.NewGuid(), "Product", quantity, unitPrice);

        // Assert
        Assert.Equal(quantity, item.Quantity);
        Assert.Equal(unitPrice, item.UnitPrice);
        Assert.False(item.Cancelled);
        Assert.True(item.TotalAmount > 0);
    }

    [Fact(DisplayName = "Should throw ArgumentException when quantity exceeds 20")]
    public void SaleItem_Creation_ShouldThrow_ForQuantityAboveLimit()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var item = new SaleItem(Guid.NewGuid(), "Product", 21, 100m);
        });
    }

    [Theory(DisplayName = "Should calculate discount correctly based on quantity")]
    [InlineData(1, 100, 0)] // no discount
    [InlineData(3, 100, 0)] // no discount
    [InlineData(4, 100, 40)] // 10% discount
    [InlineData(9, 100, 90)] // 10% discount
    [InlineData(10, 100, 200)] // 20% discount
    [InlineData(20, 100, 400)] // 20% discount
    public void SaleItem_ShouldCalculateDiscountCorrectly(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        // Arrange & Act
        var item = new SaleItem(Guid.NewGuid(), "Product", quantity, unitPrice);

        // Assert
        Assert.Equal(expectedDiscount, item.Discount);
        Assert.Equal(quantity * unitPrice - expectedDiscount, item.TotalAmount);
    }

    [Fact(DisplayName = "Update should change quantity, unit price and discount")]
    public void SaleItem_Update_ShouldModifyPropertiesCorrectly()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product", 1, 10m);

        // Act
        item.Update(5, 20m);

        // Assert
        Assert.Equal(5, item.Quantity);
        Assert.Equal(20m, item.UnitPrice);
        Assert.Equal(10m, item.Discount); // 5 * 20 * 0.1 = 10 discount
        Assert.False(item.Cancelled);
    }

    [Fact(DisplayName = "Update should throw exception if quantity > 20")]
    public void SaleItem_Update_ShouldThrow_ForQuantityAboveLimit()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product", 1, 10m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            item.Update(21, 10m);
        });
    }

    [Fact(DisplayName = "Cancel should mark item as cancelled and total amount as zero")]
    public void SaleItem_Cancel_ShouldSetCancelledAndZeroTotal()
    {
        // Arrange
        var item = new SaleItem(Guid.NewGuid(), "Product", 5, 10m);

        // Act
        item.Cancel();

        // Assert
        Assert.True(item.Cancelled);
        Assert.Equal(0m, item.TotalAmount);
    }
}