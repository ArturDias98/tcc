using System.Text.Json.Serialization;

namespace FuzzyClient.Service.Services.Models;

public record FuzzyResponseModel
{
    [JsonPropertyName("valve_opening")]
    public double Percentage { get; set; }
}