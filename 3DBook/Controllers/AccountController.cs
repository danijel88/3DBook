using _3DBook.UseCases.AccountsAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator")]
public class AccountController(IAccountService accountService) : Controller
{
    private readonly IAccountService _accountService = accountService;

    public async Task<IActionResult> Index()
    {
        var users = await _accountService.GetAllAccountsAsync();
        return View(users);
    }
}