using Microsoft.Extensions.DependencyInjection;

namespace TCC.UI.RazorLib;

public static class DependencyInjection
{
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        return services
            .AddAntDesign();
    }
}