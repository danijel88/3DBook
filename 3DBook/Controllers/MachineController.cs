﻿using _3DBook.Core.MachineAggregate;
using _3DBook.Models.AccountViewModel;
using _3DBook.UseCases.Dtos.MachineViewModel;
using _3DBook.UseCases.MachineAggregate;
using _3DBook.Validators.MachineAggregate.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator,Manager,Member,Operator")]
public class MachineController(IMachineService machineService, ILogger<MachineController> logger) : Controller
{
    private readonly IMachineService _machineService = machineService;
    private readonly ILogger<MachineController> _logger = logger;

    [HttpGet("Machine/Index")]
    public async Task<IActionResult> Index()
    {
        var machines = await _machineService.ListAsync();
        return View(machines);
    }
    [Authorize(Roles = "Administrator, Manager")]
    [HttpGet("Machine/Create")]
    public IActionResult Create()
    {

        return View();
    }

    [Authorize(Roles = "Administrator, Manager")]
    [HttpPost("Machine/Create")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateMachineViewModel model)
    {
        var validator = new CreateMachineValidator();
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View(model);
        }

        var response = await _machineService.CreateAsync(model);
        if (!response.IsSuccess)
        {
            ModelState.AddModelError("Error","Fail to save");
            return View(model);
        }
        return RedirectToAction("Index", "Machine");
    }
}