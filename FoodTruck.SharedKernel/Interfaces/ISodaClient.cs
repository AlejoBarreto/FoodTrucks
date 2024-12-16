using SODA;

namespace FoodTruck.SharedKernel.Interfaces;
public interface ISodaClient
{
    Resource<T> GetResource<T>(string resourceId) where T : class;
}
