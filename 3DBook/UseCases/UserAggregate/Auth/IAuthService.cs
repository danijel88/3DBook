using _3DBook.Models.AuthViewModel;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace _3DBook.UseCases.UserAggregate.Auth;

public interface IAuthService
{
    Task<Result> LoginAsync(LoginViewModel model);
    Task LogoutAsync();
}