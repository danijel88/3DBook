using _3DBook.Infrastructure;

namespace _3DBook.Utils.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services,
        Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
    {
        services.AddInfrastructureServices(builder.Configuration,logger)
            .AddMediatrConfigs();

        return services;
    }
}