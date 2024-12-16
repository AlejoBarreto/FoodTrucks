using FoodTruck.Infrastructure.Clients;
using FoodTruck.SharedKernel.Interfaces;

namespace FoodTruck.Api.Extensions;
public static class ClientCollectionExtensions
{
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddScoped<ISodaClient, SodaClientWrapper>();
        services.AddSingleton<IFoodTrucksClient, FoodTrucksClient>();

        return services;
    }
}

