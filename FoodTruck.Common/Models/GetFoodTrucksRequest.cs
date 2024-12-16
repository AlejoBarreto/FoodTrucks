namespace FoodTruck.Common.Models;
public class GetFoodTrucksRequest
{
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public int Radius { get; set; }
}

