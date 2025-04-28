namespace FuzzyClient.Service.Services;

public interface IApiService
{
    Task<bool> IsHealthAsync(CancellationToken cancellation = default);
    Task<double> CalculateAsync(double percentage, CancellationToken cancellation = default);
}