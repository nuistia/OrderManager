using Microsoft.Extensions.Hosting;

namespace Hometask.OrderManager.App;

public class Program
{
    static void Main(string[] args)
    {
        HostBuilder hostBuilder = new();

        DependencyInjection.ConfigureHost(hostBuilder);

        using IHost host = hostBuilder.Build();
    }
}
