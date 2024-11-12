using _3DBook.Models.AccountViewModel;

namespace _3DBook.UseCases.AccountsAggregate;

public interface IAccountService
{
    Task<List<AccountsViewModel>> GetAllAccountsAsync();
}