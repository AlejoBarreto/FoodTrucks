using FoodTruck.Common.Models;
using FoodTruck.SharedKernel.Interfaces;

namespace FoodTruck.Core.Services;
public class FoodTruckService : IFoodTruckService
{
    private readonly IFoodTruckRepository _foodTruckRepository;
    public FoodTruckService(IFoodTruckRepository foodTruckRepository)
    {
        _foodTruckRepository = foodTruckRepository;
    }

    public async Task<List<GetFoodTruckResponse>?> GetFoodTrucksAsync(GetFoodTrucksRequest request)
    {
        return await _foodTruckRepository.GetFoodTrucksAsync(request);
    }

    public List<GetFoodTruckResponse>? GetFoodTrucksInRadius(GetFoodTrucksRequest request)
    {
        return _foodTruckRepository.GetFoodTrucksInRadius(request);
    }
}

