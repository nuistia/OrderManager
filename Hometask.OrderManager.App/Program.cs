using Hometask.OrderManager.App;
using Microsoft.Extensions.Hosting;

HostBuilder hostBuilder = new();

DependencyInjection.ConfigureHost(hostBuilder);

using IHost host = hostBuilder.Build();
