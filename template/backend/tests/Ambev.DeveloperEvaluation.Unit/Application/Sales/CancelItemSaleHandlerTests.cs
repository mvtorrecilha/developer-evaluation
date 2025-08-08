using Ambev.DeveloperEvaluation.Application.Sales.CancelItemSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelItemSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly CancelItemSaleHandler _handler;
    private readonly ILogger<CancelItemSaleHandler> _logger;

    public CancelItemSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _logger = Substitute.For<ILogger<CancelItemSaleHandler>>();
        _handler = new CancelItemSaleHandler(_saleRepository, _logger);
    }

    [Fact(DisplayName = "Given valid command When handling Then cancels the item successfully")]
    public async Task Handle_ValidCommand_CancelsItemSuccessfully()
    {
        // Given
        var sale = CancelItemSaleHandlerTestsData.GenerateValidSaleWithItems();
        var command = CancelItemSaleHandlerTestsData.GenerateValidCommand(sale);

        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new Sale(sale.CustomerId, sale.CustomerName, sale.BranchId, sale.BranchName)));

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.SaleId.Should().Be(sale.Id);
        result.ItemId.Should().Be(command.ItemId);

        // Procura item e verifica se foi marcado como cancelado
        var canceledItem = sale.Items.FirstOrDefault(i => i.ProductId == command.ItemId);
        canceledItem.Should().NotBeNull();
        canceledItem.Cancelled.Should().BeFalse();

        await _saleRepository.Received(1).GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale id When handling Then throws KeyNotFoundException")]
    public async Task Handle_InvalidSaleId_ThrowsKeyNotFoundException()
    {
        // Given
        var command = CancelItemSaleHandlerTestsData.GenerateCommandWithInvalidSaleId();

        _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns((Sale)null);

        // When
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with Id {command.SaleId} not found.");

        await _saleRepository.Received(1).GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>());
        await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid item id When handling Then returns success without cancelling any item")]
    public async Task Handle_InvalidItemId_ReturnsSuccessWithoutCancel()
    {
        // Given
        var sale = CancelItemSaleHandlerTestsData.GenerateValidSaleWithItems();
        var command = CancelItemSaleHandlerTestsData.GenerateCommandWithInvalidItemId(sale);

        _saleRepository.GetByIdAsync(sale.Id, Arg.Any<CancellationToken>())
           .Returns(sale);

        _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new Sale(sale.CustomerId, sale.CustomerName, sale.BranchId, sale.BranchName)));

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.SaleId.Should().Be(sale.Id);
        result.ItemId.Should().Be(command.ItemId);

        var canceledItem = sale.Items.Find(i => i.Id == command.ItemId);
        canceledItem.Should().BeNull();

        await _saleRepository.Received(1).GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>());
        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
    }
}