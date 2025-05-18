using System.Text.Json.Serialization;

namespace FuzzyClient.Service.Services.Models;

public record FuzzyRequestModel
{
    [JsonPropertyName("level")] public double Level { get; set; } = 0;
    [JsonPropertyName("rate")] public double Rate { get; set; } = 0;
}