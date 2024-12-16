using FoodTruck.Common.Models;
using FoodTruck.Core.Services;
using FoodTruck.SharedKernel.Interfaces;
using NSubstitute;

namespace FoodTrucks.Tests.Services;
public class FoodTruckServiceTests
{
    private IFoodTruckRepository _repository = null!;
    private IFoodTruckService _service = null!;
    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IFoodTruckRepository>();

        _service = new FoodTruckService(_repository);
    }

    [Test]
    public async Task GetFoodTrucksAsync_Success()
    {
        // Arrange
        var request = new GetFoodTrucksRequest() { Latitude = "10", Longitude = "10", Radius = 10 };

        _repository.GetFoodTrucksAsync(request)
            .Returns(new List<GetFoodTruckResponse>()
            {
                new () { Location = "location_1" },
                new () { Location = "location_2" }
            });

        // Act
        var response = await _service.GetFoodTrucksAsync(request);

        // Assert
        Assert.That(response, Is.Not.Null);
    }

    [Test]
    public void GetFoodTrucksInRadius_ReturnsListOfFoodTrucks_WhenDataExists()
    {
        // Arrange
        var request = new GetFoodTrucksRequest
        {
            Latitude = "37.78143",
            Longitude = "-122.40650",
            Radius = 500
        };

        var expectedFoodTrucks = new List<GetFoodTruckResponse>
        {
            new () { Location = "Location1" },
            new () { Location = "Location2" }
        };

        _repository.GetFoodTrucksInRadius(request).Returns(expectedFoodTrucks);

        // Act
        var result = _service.GetFoodTrucksInRadius(request);

        // Assert
        Assert.NotNull(result);
    }

}

