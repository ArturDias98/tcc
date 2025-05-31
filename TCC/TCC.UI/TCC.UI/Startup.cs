using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TCC.Core;
using TCC.UI.RazorLib;

namespace TCC.UI
{
    public static class Startup
    {
        public static IServiceProvider? Services { get; private set; }

        public static void Init()
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices(WireupServices)
                           .Build();
            Services = host.Services;
            host.Start();
        }

        private static void WireupServices(IServiceCollection services)
        {
            services.AddWpfBlazorWebView();
            services
                .AddCoreServices()
                .AddUiServices();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif
        }
    }
}
