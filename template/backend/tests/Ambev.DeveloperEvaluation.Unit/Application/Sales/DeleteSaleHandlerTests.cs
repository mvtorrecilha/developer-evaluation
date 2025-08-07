using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleHandler _handler;

    public DeleteSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleHandler(_saleRepository);
    }

    [Fact(DisplayName = "Given existing sale When deleting Then returns success result and calls DeleteAsync")]
    public async Task GivenExistingSale_WhenDeleting_ThenReturnsSuccessAndCallsDelete()
    {
        // Given
        var sale = DeleteSaleHandlerTestsData.GenerateValidSale();
        var command = new DeleteSaleCommand(sale.Id);

        _saleRepository.GetByIdAsync(command.Id).Returns(sale);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Success.Should().BeTrue();
        await _saleRepository.Received(1).DeleteAsync(sale, Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given non-existing sale When deleting Then returns failure and does not call DeleteAsync")]
    public async Task GivenNonExistingSale_WhenDeleting_ThenReturnsFailureAndDoesNotCallDelete()
    {
        // Given
        var nonExistentId = DeleteSaleHandlerTestsData.GenerateRandomSaleId();
        var command = new DeleteSaleCommand(nonExistentId);

        _saleRepository.GetByIdAsync(nonExistentId).Returns((Sale)null);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Success.Should().BeFalse();
        await _saleRepository.DidNotReceive().DeleteAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
    }
}