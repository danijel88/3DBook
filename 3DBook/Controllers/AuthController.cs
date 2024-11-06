using _3DBook.Core;
using _3DBook.Models.AuthViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

[AllowAnonymous]
public class AuthController(SignInManager<User> signInManager,ILogger<AuthController> logger) : Controller
{
    private SignInManager<User> _signInManager = signInManager;
    private ILogger<AuthController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = null)
    {
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User {model.Email} is logged. ");
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("Error","Invalid login attempt.");
                return View(model);
            }
        }
        return View(model);
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