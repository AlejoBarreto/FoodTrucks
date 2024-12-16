using System.Net;
using System.Text.Json;
using FoodTruck.Common.Models;
using FoodTruck.Infrastructure.Clients;
using NSubstitute;

namespace FoodTrucks.Tests.Clients;
public class FoodTruckClientTests
{
    private IHttpClientFactory _httpClientFactory = null!;
    private FoodTrucksClient _client = null!;

    [SetUp]
    public void SetUp()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();

        _client = new FoodTrucksClient(_httpClientFactory);
    }

    [Test]
    public async Task GetFoodTrucks_ShouldReturnValidResult()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpResponseListMessageHandler();

        var httpClient = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        // Act
        var result = await _client.GetFoodTrucksAsync(new GetFoodTrucksRequest() { Latitude = "10", Longitude = "10", Radius = 10 });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            _httpClientFactory.Received(1).CreateClient(Arg.Any<string>());
        });
    }

    [Test]
    public async Task GetFoodTrucks_ShouldReturnNullWhenError()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpResponseFailHandler();

        var httpClient = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        // Act
        var result = await _client.GetFoodTrucksAsync(new GetFoodTrucksRequest() { Latitude = "10", Longitude = "10", Radius = 10 });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _httpClientFactory.Received(1).CreateClient(Arg.Any<string>());
        });
    }
}

internal class MockHttpResponseListMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(new List<GetFoodTruckResponse>()))
        });
    }
}

internal class MockHttpResponseFailHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest
        });
    }
}