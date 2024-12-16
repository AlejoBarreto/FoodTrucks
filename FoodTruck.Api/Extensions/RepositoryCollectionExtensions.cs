using FoodTruck.Infrastructure.Repositories;
using FoodTruck.SharedKernel.Interfaces;

namespace FoodTruck.Api.Extensions;
public static class RepositoryCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();
        return services;
    }
}

