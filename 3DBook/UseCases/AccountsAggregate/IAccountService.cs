using _3DBook.Models.AccountViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.AccountsAggregate;

public interface IAccountService
{
    Task<List<AccountsViewModel>> GetAllAccountsAsync();
    Task<Result> CreateUserAsync(CreateAccountViewModel model);
    Task<Result> ChangePassword(ChangePasswordViewModel model,string email);
    Task<Result> ForgotPassword(string email, string scheme);
    Task<Result> ResetPassword(string email,string token, string password);
}