using System.Net.NetworkInformation;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager config,ILogger logger)
    {
        string? connectionString = config.GetConnectionString("DefaultConnection");
        GuardClauses.GuardClause.IsNullOrEmptyString(connectionString,nameof(connectionString));
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        logger.LogInformation("{Project} services registered","Infrastructure");

        return services;
    }
}