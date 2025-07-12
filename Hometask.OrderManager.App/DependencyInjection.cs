using Hometask.OrderManager.Core.Services.EF.Orders;
using Hometask.OrderManager.Data.EF;
using Hometask.OrderManager.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hometask.OrderManager.App;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<CHIITDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }

    public static void ConfigureHost(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            config
                .AddJsonFile(@"appsettings.json")
                .AddJsonFile($@"appsettings.{environment}.json");
        });

        hostBuilder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.RegisterServices(configuration);
        });
    }
}
