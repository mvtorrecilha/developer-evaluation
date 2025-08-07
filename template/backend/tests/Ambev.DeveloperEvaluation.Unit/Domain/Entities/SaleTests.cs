using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleTests
{
    [Fact(DisplayName = "Sale should initialize with valid customer and branch data")]
    public void Sale_Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();

        // Assert
        Assert.NotEqual(Guid.Empty, sale.CustomerId);
        Assert.NotEqual(Guid.Empty, sale.BranchId);
        Assert.False(string.IsNullOrWhiteSpace(sale.CustomerName));
        Assert.False(string.IsNullOrWhiteSpace(sale.BranchName));
        Assert.False(sale.Cancelled);
        Assert.NotNull(sale.Items);
        Assert.Empty(sale.Items);
        Assert.Equal(0m, sale.TotalAmount);
        Assert.False(string.IsNullOrWhiteSpace(sale.SaleNumber));
    }

    [Fact(DisplayName = "Should correctly add a new item to the sale")]
    public void Sale_AddItem_ShouldAddItemAndRecalculateTotal()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var item = SaleItemTestData.GenerateValidSaleItem(2, 20m);

        // Act
        sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);

        // Assert
        Assert.Single(sale.Items);
        Assert.True(sale.TotalAmount > 0);
    }

    [Fact(DisplayName = "Should update existing items and add new ones")]
    public void Sale_UpdateItems_ShouldUpdateAndAddItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var existing = SaleItemTestData.GenerateValidSaleItem(2, 20m);
        sale.AddItem(existing.ProductId, existing.ProductName, existing.Quantity, existing.UnitPrice);

        var updatedItems = new List<(Guid, string, int, decimal)>
        {
            (existing.ProductId, existing.ProductName, 5, 30m), 
            (Guid.NewGuid(), "New Product", 3, 50m)            
        };

        // Act
        sale.UpdateItems(updatedItems);

        // Assert
        Assert.Equal(2, sale.Items.Count);
        Assert.True(sale.TotalAmount > 0);
    }

    [Fact(DisplayName = "Should cancel items not present in update list")]
    public void Sale_UpdateItems_ShouldCancelRemovedItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var item1 = SaleItemTestData.GenerateValidSaleItem(2, 20m);
        var item2 = SaleItemTestData.GenerateValidSaleItem(2, 20m);
        sale.AddItem(item1.ProductId, item1.ProductName, item1.Quantity, item1.UnitPrice);
        sale.AddItem(item2.ProductId, item2.ProductName, item2.Quantity, item2.UnitPrice);

        var updatedItems = new List<(Guid, string, int, decimal)>
        {
            (item1.ProductId, item1.ProductName, item1.Quantity, item1.UnitPrice)
        };

        // Act
        sale.UpdateItems(updatedItems);

        // Assert
        var canceledItem = sale.Items.First(i => i.ProductId == item2.ProductId);
        Assert.True(canceledItem.Cancelled);
    }

    [Fact(DisplayName = "Should mark the sale as cancelled")]
    public void Sale_CancelSale_ShouldSetCancelledFlag()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();

        // Act
        sale.CancelSale();

        // Assert
        Assert.True(sale.Cancelled);
    }

    [Fact(DisplayName = "Should cancel a specific item by ID")]
    public void Sale_CancelItem_ShouldCancelTheCorrectItem()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var item = SaleItemTestData.GenerateValidSaleItem(2, 20m);
        sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
        var addedItem = sale.Items.First();

        // Act
        sale.CancelItem(addedItem.Id);

        // Assert
        Assert.True(addedItem.Cancelled);
        Assert.Equal(0m, addedItem.TotalAmount);
    }

    [Fact(DisplayName = "Total amount should exclude cancelled items")]
    public void Sale_RecalculateTotal_ShouldIgnoreCancelledItems()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var item1 = SaleItemTestData.GenerateValidSaleItem(5, 10m);
        var item2 = SaleItemTestData.GenerateValidSaleItem(2, 20m);

        sale.AddItem(item1.ProductId, item1.ProductName, item1.Quantity, item1.UnitPrice);
        sale.AddItem(item2.ProductId, item2.ProductName, item2.Quantity, item2.UnitPrice);

        var originalTotal = sale.TotalAmount;
        var cancelId = sale.Items.First(i => i.ProductId == item2.ProductId).Id;

        // Act
        sale.CancelItem(cancelId);

        // Assert
        Assert.True(sale.TotalAmount < originalTotal);
    }

    [Fact(DisplayName = "CancelItem should not throw if item ID does not exist")]
    public void Sale_CancelItem_ShouldNotThrowIfItemNotFound()
    {
        // Arrange
        var sale = SaleTestData.GenerateBasicSale();
        var randomId = Guid.NewGuid();

        // Act & Assert
        var exception = Record.Exception(() => sale.CancelItem(randomId));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Creating SaleItem with quantity above limit should throw exception")]
    public void Constructor_ShouldThrowArgumentException_WhenQuantityAboveLimit()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            var invalidItem = SaleTestData.GenerateInvalidSaleItem_QuantityAboveLimit();
        });

        Assert.Equal("Cannot sell more than 20 items", exception.Message);
    }

    [Fact(DisplayName = "Updating SaleItem to quantity above limit should throw exception")]
    public void Update_ShouldThrowArgumentException_WhenQuantityAboveLimit()
    {
        // Arrange
        var item = SaleItemTestData.GenerateValidSaleItem(5, 10m);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
        {
            item.Update(21, 10m);
        });

        Assert.Equal("Cannot sell more than 20 items", exception.Message);
    }
}