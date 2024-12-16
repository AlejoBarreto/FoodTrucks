namespace FoodTruck.Common.Models;
public class SodaClientSettings
{
    public const string OptionKey = "SodaClientSettings";
    public string Host { get; set; } = string.Empty;
    public string AppToken { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}