using System.Text.Json.Serialization;

namespace FuzzyClient.Service.Services.Models;

public record FuzzyRequestModel
{
    [JsonPropertyName("error")] public double Error { get; set; } = 0;
}