using System.Web;
using _3DBook.Core;
using _3DBook.Core.Interfaces;
using _3DBook.Models.AccountViewModel;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.UseCases.AccountsAggregate;

public class AccountService(UserManager<User> userManager,ILogger<AccountService> logger, IEmailSender emailSender) : IAccountService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ILogger<AccountService> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;
    /// <inheritdoc />
    public async Task<List<AccountsViewModel>> GetAllAccountsAsync()
    {
        _logger.LogInformation("Getting all users.");
        var users = await _userManager.Users.Select(s => new AccountsViewModel
        {
            Company = s.Company,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email!,
            IsActive = s.IsActive
        }).ToListAsync();
        return users;
    }

    /// <inheritdoc />
    public async Task<Result> CreateUserAsync(CreateAccountViewModel model)
    {
        _logger.LogInformation("Creating new user");

           var identityResult = await _userManager.CreateAsync(new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Company = model.Company,
                IsActive = true,
                UserName = model.Email
            }, model.Password);
            if (!identityResult.Succeeded)
            {
                return Result.Error("user not saved");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _userManager.AddToRoleAsync(user, model.RoleName);
            _logger.LogInformation($"User {model.Email} has been created successfully");
            return Result.Success();
    }

    /// <inheritdoc />
    public async Task<Result> ChangePassword(ChangePasswordViewModel model,string email)
    {
        _logger.LogInformation($"Change password for {email}");
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.NotFound();
        }

        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (result.Succeeded)
        {
            _logger.LogInformation("Password successfully changed.");
            return Result.Success();
        }

        _logger.LogError($"There is some error on changing: {result.Errors.FirstOrDefault().Description}");

        return Result.Error(result.Errors.FirstOrDefault().Description);

    }

    public async Task<Result> ForgotPassword(string email,string scheme)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.NotFound();
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var builder = new UriBuilder("https://3dbook.calzedonia.com")
        {
            Scheme = scheme,
            Path = "/Account/ResetPassword"
        };
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["code"] = code;
        query["userId"] = user.Id;
        builder.Query = query.ToString();
        var body = builder.ToString();
        await _emailSender.SendEmailAsync(email, "no-replay@3dbook.calzedonia.com", "Reset Password",
            body);
        return Result.Success();
    }

    public async Task<Result> ResetPassword(string email,string token, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.NotFound();
        }

        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (result.Succeeded)
        {
            return Result.Success();
        }
        return Result.Error(result.Errors.ToString());
    }
}