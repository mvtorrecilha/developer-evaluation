using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<CreateSaleHandler>>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given valid sale data When handling Then returns success result")]
    public async Task Handle_ValidCommand_ReturnsSuccess()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        var sale = new Sale(command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);
        foreach (var item in command.CartItems)
            sale.AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);

        _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

        var expectedResult = new CreateSaleResult { Id = sale.Id };
        _mapper.Map<CreateSaleResult>(sale).Returns(expectedResult);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<CreateSaleResult>(Arg.Any<Sale>());
    }

    [Fact(DisplayName = "Given invalid sale data When handling Then throws validation exception")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Given
        var command = new CreateSaleCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given empty cart When handling Then throws validation exception")]
    public async Task Handle_EmptyCart_ThrowsValidationException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();
        command.CartItems.Clear();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("*At least one item must be provided*");
    }

    [Fact(DisplayName = "Given valid command When handling Then adds all items to sale")]
    public async Task Handle_ValidCommand_AddsAllItemsToSale()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        var expectedItemCount = command.CartItems.Count;

        Sale? capturedSale = null;

        _saleRepository.CreateAsync(Arg.Do<Sale>(s => capturedSale = s), Arg.Any<CancellationToken>())
            .Returns(callInfo => callInfo.Arg<Sale>());

        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(new CreateSaleResult { Id = Guid.NewGuid() });

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        capturedSale.Should().NotBeNull();
        capturedSale!.Items.Count.Should().Be(expectedItemCount);
    }

    [Fact(DisplayName = "Given repository fails When handling Then propagates exception")]
    public async Task Handle_RepositoryThrows_PropagatesException()
    {
        // Given
        var command = CreateSaleHandlerTestData.GenerateValidCommand();

        _saleRepository
            .CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
            .Throws(new InvalidOperationException("Database error"));

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Database error");
    }
}
