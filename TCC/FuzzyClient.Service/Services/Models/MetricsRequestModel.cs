using System.Text.Json.Serialization;

namespace FuzzyClient.Service.Services.Models;

public class MetricsRequestModel
{
    [JsonPropertyName("ref")] public double Reference { get; set; }
    [JsonPropertyName("tol")] public double Tolerance { get; set; }
    [JsonPropertyName("y")] public List<double> Values { get; set; } = [];
    [JsonPropertyName("t")] public List<double> Time { get; set; } = [];
}