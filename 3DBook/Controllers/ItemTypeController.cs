using _3DBook.Models.ItemTypeViewModel;
using _3DBook.UseCases.ItemAggregate;
using _3DBook.UseCases.ItemAggregate.Validator;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

[Authorize(Roles="Administrator,Manager,Member,Operator")]
public class ItemTypeController(IItemTypeService itemTypeService) : Controller
{
    private readonly IItemTypeService _itemTypeService = itemTypeService;

    [HttpGet("ItemType/Index")]
    public async Task<IActionResult> Index()
    {
        var response = await _itemTypeService.ListAsync();
        return View(response);
    }

    [HttpGet("ItemType/Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("ItemType/Create")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateItemTypeViewModel model)
    {
        CreateItemTypeValidator validator = new CreateItemTypeValidator();
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View(model);
        }
        var response = await _itemTypeService.CreateAsync(model);
        if (!response.IsSuccess)
        {
            this.ModelState.AddModelError("Error",response.Errors.FirstOrDefault() ?? "Error");
            return View(model);
        }

        return RedirectToAction("Index", "ItemType");
    }
}