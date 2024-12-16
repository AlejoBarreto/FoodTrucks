using FoodTruck.Core.Services;
using FoodTruck.SharedKernel.Interfaces;

namespace FoodTruck.Api.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFoodTruckService, FoodTruckService>();
        return services;
    }
}

