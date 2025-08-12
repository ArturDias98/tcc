using System.Text.Json.Serialization;

namespace FuzzyClient.Service.Services.Models;

public class MetricsResponseModel
{
    [JsonPropertyName("mse")] public double? MSE { get; set; }
    [JsonPropertyName("overshoot")] public double? Overshoot { get; set; }
    [JsonPropertyName("settling_time")] public double? SettlingTime { get; set; }
}