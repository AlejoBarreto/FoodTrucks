using FoodTruck.Common.Models;
using FoodTruck.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using SODA;

namespace FoodTruck.Infrastructure.Clients;
public class SodaClientWrapper : ISodaClient
{
    private readonly SodaClient _sodaClient;
    private readonly SodaClientSettings _settings;

    public SodaClientWrapper(IOptions<SodaClientSettings> settings)
    {
        _settings = settings.Value;
        _sodaClient = new SodaClient(_settings.Host, _settings.AppToken);
    }

    public Resource<T> GetResource<T>(string resourceId) where T : class
    {
        return _sodaClient.GetResource<T>(resourceId);
    }
}
