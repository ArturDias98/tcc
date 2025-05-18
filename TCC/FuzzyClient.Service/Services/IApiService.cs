namespace FuzzyClient.Service.Services;

public interface IApiService
{
    Task<bool> IsHealthAsync(CancellationToken cancellation = default);
    Task<double> CalculateAsync(double level, double rate, CancellationToken cancellation = default);
}