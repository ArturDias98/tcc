using FuzzyClient.Service;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApiServices(builder.Configuration);

var host = builder.Build();

await host.RunAsync();