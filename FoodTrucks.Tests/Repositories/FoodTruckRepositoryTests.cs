using FoodTruck.Common.Models;
using FoodTruck.Infrastructure.Repositories;
using FoodTruck.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace FoodTrucks.Tests.Repositories;
public class FoodTruckRepositoryTests
{
    private IFoodTrucksClient _client = null!;
    private IFoodTruckRepository _repository = null!;
    private ILogger<FoodTruckRepository> _logger;
    private ISodaClient _sodaClient;

    [SetUp]
    public void Setup()
    {
        _client = Substitute.For<IFoodTrucksClient>();
        _logger = Substitute.For<ILogger<FoodTruckRepository>>();
        _sodaClient = Substitute.For<ISodaClient>();

        _repository = new FoodTruckRepository(_logger, _sodaClient, _client);
    }

    [Test]
    public async Task GetFoodTrucksAsync_Success()
    {
        // Arrange
        var request = new GetFoodTrucksRequest() { Latitude = "10", Longitude = "10", Radius = 10 };
        var foodTrucks = new List<GetFoodTruckResponse>()
        {
            new () { Location = "location_1" },
            new () { Location = "location_2" }
        };

        _client.GetFoodTrucksAsync(request).Returns(foodTrucks);

        // Act
        var response = await _repository.GetFoodTrucksAsync(request);

        // Assert
        Assert.That(response, Is.Not.Null);
    }
}
