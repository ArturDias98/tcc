using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpcUaClient;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddOpcUaClient();

var host = builder.Build();

var service = host.Services.GetRequiredService<IOpcClient>();

await service.ConfigureAsync("127.0.0.1:50000");
await service.StartAsync();

service.AddMonitoredItems(["ns=2;s=Simulation Examples.Functions.Ramp1"]);
await host.StartAsync();

await host.RunAsync();
