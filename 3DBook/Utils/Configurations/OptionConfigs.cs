using _3DBook.Infrastructure.Email;
using _3DBook.UseCases.UserAggregate.Auth;

namespace _3DBook.Utils.Configurations;

public static class OptionConfigs
{
    public static IServiceCollection AddOptionConfigs(this IServiceCollection services,
        IConfiguration configuration,
        ILogger logger,
        WebApplicationBuilder builder)
    {
        services.Configure<MailServerConfiguration>(configuration.GetSection("MailServer"))
            .Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


        services.AddScoped<IAuthService, AuthService>();
        

        logger.LogInformation("{Project} were configured", "Options");
        return services;
    }   
}