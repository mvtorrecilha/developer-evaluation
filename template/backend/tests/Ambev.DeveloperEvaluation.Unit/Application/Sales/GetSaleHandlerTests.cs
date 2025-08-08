using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleHandler _handler;
    private readonly ILogger<GetSaleHandler> _logger;

    public GetSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetSaleHandler>>();
        _handler = new GetSaleHandler(_saleRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given existing sale When handling Then returns mapped sale result")]
    public async Task GivenExistingSale_WhenHandling_ThenReturnsMappedSaleResult()
    {
        // Given
        var sale = GetSaleHandlerTestsData.GenerateValidSale();
        var query = GetSaleHandlerTestsData.GenerateValidQuery(sale.Id);
        var expectedResult = new GetSaleResult
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            CustomerName = sale.CustomerName,
            BranchId = sale.BranchId,
            BranchName = sale.BranchName,
            SaleDate = DateTime.UtcNow,
            SaleItems = GetSaleHandlerTestsData.GenerateSaleItemResults(),
            TotalSaleAmount = 100m,
            IsCanceled = false
        };

        _saleRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns(sale);
        _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
        await _saleRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<GetSaleResult>(sale);
    }

    [Fact(DisplayName = "Given non-existent sale When handling Then throws KeyNotFoundException")]
    public async Task GivenNonExistentSale_WhenHandling_ThenThrowsKeyNotFoundException()
    {
        // Given
        var query = GetSaleHandlerTestsData.GenerateValidQuery(Guid.NewGuid());
        _saleRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns((Sale)null);

        // When
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Then
        await act.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Sale with Id {query.Id} not found.");

        await _saleRepository.Received(1).GetByIdAsync(query.Id, Arg.Any<CancellationToken>());
        _mapper.DidNotReceive().Map<GetSaleResult>(Arg.Any<Sale>());
    }
}