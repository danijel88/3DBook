using _3DBook.Models.AuthViewModel;
using _3DBook.UseCases.UserAggregate.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

[AllowAnonymous]
public class AuthController(IAuthService authService) : Controller
{
    private IAuthService _authService = authService;

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(model);
            if (result.IsSuccess)
            {
                if (returnUrl != null) return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Error","Invalid login attempt.");
                return View(model);
            }
        }
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}