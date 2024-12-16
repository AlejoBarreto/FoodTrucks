using FoodTruck.Common.Models;

namespace FoodTruck.SharedKernel.Interfaces;
public interface IFoodTruckRepository
{
    List<GetFoodTruckResponse>? GetFoodTrucksInRadius(GetFoodTrucksRequest request);
    Task<List<GetFoodTruckResponse>?> GetFoodTrucksAsync(GetFoodTrucksRequest request);
}

