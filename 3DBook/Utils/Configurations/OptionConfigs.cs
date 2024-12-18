﻿using _3DBook.Core.Interfaces;
using _3DBook.Core.MachineAggregate;
using _3DBook.Infrastructure.Email;
using _3DBook.Models.AccountViewModel;
using _3DBook.UseCases.AccountsAggregate;
using _3DBook.UseCases.FolderAggregate;
using _3DBook.UseCases.ItemAggregate;
using _3DBook.UseCases.MachineAggregate;
using _3DBook.UseCases.UserAggregate.Auth;
using _3DBook.Validators.AccountsAggregate.Validators;
using FluentValidation;


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
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<IFolderService, FolderService>();
        services.AddScoped<IChildrenService, ChildrenService>();
        services.AddScoped<IItemTypeService, ItemTypeService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IEmailSender, SmtpEmailSender>();

        services.AddScoped<IValidator<CreateAccountViewModel>, CreateAccountValidator>();
        
        logger.LogInformation("{Project} were configured", "Options");
        return services;
    }   
}