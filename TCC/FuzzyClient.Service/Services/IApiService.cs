using TCC.Shared.Models.Metrics;

namespace FuzzyClient.Service.Services;

public interface IApiService
{
    Task<bool> IsHealthAsync(CancellationToken cancellation = default);
    Task<double> CalculateAsync(double level, double rate, CancellationToken cancellation = default);

    Task<MetricsModel> GetMetricsAsync(
        double reference,
        double tolerance,
        List<double> y,
        List<double> t,
        CancellationToken cancellation = default);
}