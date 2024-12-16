using FoodTruck.Common.Models;
using FoodTruck.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using SODA;

namespace FoodTruck.Infrastructure.Repositories;
public class FoodTruckRepository : IFoodTruckRepository
{
    private readonly ISodaClient _sodaClient;
    private readonly IFoodTrucksClient _foodTrucksClient;
    private readonly ILogger<FoodTruckRepository> _logger;
    public FoodTruckRepository(ILogger<FoodTruckRepository> logger, ISodaClient sodaClient, IFoodTrucksClient foodTrucksClient)
    {
        _sodaClient = sodaClient;
        _logger = logger;
        _foodTrucksClient = foodTrucksClient;
    }
    public List<GetFoodTruckResponse>? GetFoodTrucksInRadius(GetFoodTrucksRequest request)
    {
        _logger.LogInformation("FoodTruck repository. Get FoodTrucks starts.");

        //get a reference to the resource itself
        var dataset = _sodaClient.GetResource<GetFoodTruckResponse>("jjew-r69b");

        // https://data.sfgov.org/resource/jjew-r69b.json
        //get near locations with specified radius
        var soql = new SoqlQuery()
            .Where($"within_circle(location_2, {request.Latitude}, {request.Longitude}, {request.Radius})");

        var results = dataset.Query(soql);

        _logger.LogInformation("FoodTruck repository. Get FoodTrucks ends.");
        return results?.DistinctBy(x => x.Location).ToList();
    }

    public async Task<List<GetFoodTruckResponse>?> GetFoodTrucksAsync(GetFoodTrucksRequest request)
    {
        return await _foodTrucksClient.GetFoodTrucksAsync(request);
    }
}