using FoodTruck.Common.Models;

namespace FoodTruck.SharedKernel.Interfaces;
public interface IFoodTrucksClient
{
    Task<List<GetFoodTruckResponse>?> GetFoodTrucksAsync(GetFoodTrucksRequest request);
}