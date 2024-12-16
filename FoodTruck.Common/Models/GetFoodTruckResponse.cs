using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SODA.Models;

namespace FoodTruck.Common.Models;
[DataContract]
public class GetFoodTruckResponse
{
    [JsonPropertyName("optionaltext")]
    public string OptionalText { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;


    [JsonPropertyName("location_2")]
    public LocationColumn LocationData { get; set; } = new();
}

