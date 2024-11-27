using _3DBook.Core.MachineAggregate;
using _3DBook.UseCases.Dtos.FolderViewModel;
using _3DBook.UseCases.FolderAggregate;
using _3DBook.UseCases.MachineAggregate;
using _3DBook.Validators.FolderAggregate.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator,Member, Operator,Manager")]
public class FolderController(IFolderService folderService,ILogger<FolderController> logger, IMachineService machineService) : Controller
{
    private readonly IFolderService _folderService = folderService;
    private readonly ILogger<FolderController> _logger = logger;
    private readonly IMachineService _machineService = machineService;

    [HttpGet("Folder/Index")]
    public async Task<IActionResult> Index()
    {
        var folders = await _folderService.ListAsync();
        return View(folders);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Folder/Create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Machines = await GetMachines();
        return View();
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Folder/Create")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateFolderViewModel model)
    {
        var validator = new CreateFolderValidator();
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            ViewBag.Machines = await GetMachines();
            return View(model);
        }

        var response = await _folderService.CreateAsync(model);
        if (!response.IsSuccess)
        {
            ModelState.AddModelError("Error","Failed to save.");
            return View(model);
        }
        return RedirectToAction("Index", "Folder");
    }

    private async Task<List<SelectListItem>> GetMachines()
    {
        var items = new List<SelectListItem>();
        var machines = await _machineService.ListAsync();
        foreach (var machine in machines)
        {
            items.Add(new SelectListItem(machine.Name,machine.Id.ToString()));
        }

        return items;
    }
}