using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CancelSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new CancelSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given valid command When handling Then cancels the sale successfully")]
    public async Task Handle_ValidCommand_CancelsSaleSuccessfully()
    {
        // Given
        var sale = CancelSaleHandlerTestsData.GenerateValidSale();
        var command = CancelSaleHandlerTestsData.GenerateValidCommand(sale.Id);

        _saleRepository.GetByIdAsync(command.Id).Returns(sale);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        result.Success.Should().BeTrue();

        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given nonexistent sale id When handling Then throws KeyNotFoundException")]
    public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
    {
        // Given
        var command = CancelSaleHandlerTestsData.GenerateValidCommand(Guid.NewGuid());

        _saleRepository.GetByIdAsync(command.Id).Returns((Sale)null);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with Id {command.Id} not found.");

        await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given already cancelled sale When handling Then does not throw and updates sale")]
    public async Task Handle_AlreadyCancelledSale_StillUpdatesSale()
    {
        // Given
        var sale = CancelSaleHandlerTestsData.GenerateValidSale();
        sale.CancelSale();

        var command = CancelSaleHandlerTestsData.GenerateValidCommand(sale.Id);
        _saleRepository.GetByIdAsync(command.Id).Returns(sale);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Id.Should().Be(sale.Id);

        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
    }
}