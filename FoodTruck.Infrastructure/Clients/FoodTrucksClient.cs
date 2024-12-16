using System.Net;
using System.Net.Http.Json;
using FoodTruck.Common.Models;
using FoodTruck.SharedKernel.Interfaces;

namespace FoodTruck.Infrastructure.Clients;
public class FoodTrucksClient : IFoodTrucksClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string ClientName = "FoodTrucksApiClient";
    public FoodTrucksClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<List<GetFoodTruckResponse>?> GetFoodTrucksAsync(GetFoodTrucksRequest request)
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync(
            $"jjew-r69b.json?$select=location,optionalText,location_2&$where=within_circle(location_2, {request.Latitude}, {request.Longitude}, {request.Radius})");

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        var foodTrucks = await response.Content.ReadFromJsonAsync<List<GetFoodTruckResponse>>();
        return foodTrucks;
    }
}

