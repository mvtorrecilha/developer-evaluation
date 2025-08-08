using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetAllSalesHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSalesHandler> _logger;
    private readonly GetAllSalesHandler _handler;

    public GetAllSalesHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllSalesHandler>>();
        _handler = new GetAllSalesHandler(_saleRepository, _mapper, _logger);
    }

    [Fact(DisplayName = "Given existing sales When handling query Then returns mapped results")]
    public async Task Handle_WithSales_ReturnsMappedResults()
    {
        // Given
        var sales = GetAllSalesHandlerTestsData.GenerateValidSales(2);
        var expectedResults = GetAllSalesHandlerTestsData.MapToResult(sales).ToList();

        _saleRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(sales);
        _mapper.Map<IEnumerable<GetAllSalesResult>>(sales).Returns(expectedResults);

        var query = new GetAllSalesQuery();

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expectedResults);

        await _saleRepository.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<IEnumerable<GetAllSalesResult>>(sales);
    }

    [Fact(DisplayName = "Given no sales When handling query Then returns empty list")]
    public async Task Handle_NoSales_ReturnsEmptyList()
    {
        // Given
        var sales = new List<Sale>();
        var expectedResults = new List<GetAllSalesResult>();

        _saleRepository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(sales);
        _mapper.Map<IEnumerable<GetAllSalesResult>>(sales).Returns(expectedResults);

        var query = new GetAllSalesQuery();

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEmpty();

        await _saleRepository.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
        _mapper.Received(1).Map<IEnumerable<GetAllSalesResult>>(sales);
    }

    [Fact(DisplayName = "Given exception thrown by repository When handling query Then propagates exception")]
    public async Task Handle_RepositoryThrows_ThrowsException()
    {
        // Given
        _saleRepository.GetAllAsync(Arg.Any<CancellationToken>()).Throws(new Exception("Database error"));
        var query = new GetAllSalesQuery();

        // When
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        await _saleRepository.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
        _mapper.DidNotReceive().Map<IEnumerable<GetAllSalesResult>>(Arg.Any<IEnumerable<Sale>>());
    }
}