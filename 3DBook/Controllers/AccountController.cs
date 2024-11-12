using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

public class AccountController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}