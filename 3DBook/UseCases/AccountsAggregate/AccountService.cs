using _3DBook.Core;
using _3DBook.Models.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.UseCases.AccountsAggregate;

public class AccountService(UserManager<User> userManager) : IAccountService
{
    private readonly UserManager<User> _userManager = userManager;
    /// <inheritdoc />
    public async Task<List<AccountsViewModel>> GetAllAccountsAsync()
    {
        var users = await _userManager.Users.Select(s => new AccountsViewModel
        {
            Company = s.Company,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            IsActive = s.IsActive
        }).ToListAsync();
        return users;
    }
}