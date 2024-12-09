﻿using _3DBook.Models.AccountViewModel;
using _3DBook.UseCases.AccountsAggregate;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator")]
public class AccountController(IAccountService accountService, IValidator<CreateAccountViewModel> createAccountValidator, RoleManager<IdentityRole> roleManager) : Controller
{
    private readonly IAccountService _accountService = accountService;
    private readonly IValidator<CreateAccountViewModel> _createAccountValidator = createAccountValidator;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    [HttpGet("Account/Index")]
    public async Task<IActionResult> Index()
    {

        var users = await _accountService.GetAllAccountsAsync();
        return View(users);
    }

    [HttpGet("Account/Create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Roles = await GetRoles();
        return View();
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateAccountViewModel model)
    {
        ValidationResult result = await _createAccountValidator.ValidateAsync(model);
        if (!result.IsValid)
        {
            ViewBag.Roles = await GetRoles();
            result.AddToModelState(this.ModelState);
            return View(model);
        }

        var createUserResult = await _accountService.CreateUserAsync(model);
        if (createUserResult.IsSuccess) { return RedirectToAction("Index", "Account"); }

        return View(model);
    }

    [Authorize]
    [HttpGet("Account/ChangePassword")]
    public IActionResult ChangePassword()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      
        var result = await _accountService.ChangePassword(model,User.Identity.Name);
        if (!result.IsSuccess)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Error", error);
            }
            return View(model);
        }
        return RedirectToAction("ChangePasswordConfirmation", "Account");
    }
    [Authorize]
    [HttpGet]
    public IActionResult ChangePasswordConfirmation()
    {
        return View();
    }

    private async Task<List<SelectListItem>> GetRoles()
    {
        var roleList = new List<SelectListItem>();
        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var role in roles)
        {
            roleList.Add(new SelectListItem(role.Name, role.Name));
        }

        return roleList;
    }
}