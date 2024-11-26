using System.Net.NetworkInformation;
using _3DBook.Core;
using _3DBook.Core.MachineAggregate;
using Ardalis.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager config,ILogger logger)
    {
        string? connectionString = config.GetConnectionString("DefaultConnection");
        GuardClauses.GuardClause.IsNullOrEmptyString(connectionString,nameof(connectionString));
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
            .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        services.ConfigureApplicationCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    // Get the requested URL
                    var returnUrl = context.Request.Path + context.Request.QueryString;

                    // Set the redirect URL including the returnUrl as a query parameter
                    var loginUrl = $"/Auth/Login?ReturnUrl={Uri.EscapeDataString(returnUrl)}";

                    // Redirect to the login page
                    context.Response.Redirect(loginUrl);
                    return Task.CompletedTask;
                }
            };
        });


        logger.LogInformation("{Project} services registered","Infrastructure");

        return services;
    }
}