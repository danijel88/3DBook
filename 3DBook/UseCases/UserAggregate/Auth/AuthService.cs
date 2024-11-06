using _3DBook.Controllers;
using _3DBook.Core;
using _3DBook.Models.AuthViewModel;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.UseCases.UserAggregate.Auth;

public class AuthService(SignInManager<User> signInManager, ILogger<AuthService> logger) : IAuthService
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly ILogger<AuthService> _logger = logger;
    /// <inheritdoc />
    public async Task<Result> LoginAsync(LoginViewModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        if (result.Succeeded)
        {
            _logger.LogInformation($"User {model.Email} has been logged");
            return Result.Success();
        }
        return Result.Error("Invalid login attempt.");
    }

    /// <inheritdoc />
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
    }
}