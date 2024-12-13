using _3DBook.Core.FolderAggregate;
using _3DBook.UseCases.FolderAggregate;
using _3DBook.UseCases.ItemAggregate;
using _3DBook.UseCases.MachineAggregate;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using _3DBook.Core.MachineAggregate;
using _3DBook.UseCases.Dtos.ItemViewModel;
using _3DBook.Validators.ItemAggregate.Validator;
using FluentValidation.AspNetCore;

namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator,Manager,Member,Operator")]
public class ItemController(IItemService itemService,
    IWebHostEnvironment webHostEnvironment,
    IMachineService machineService,
    IItemTypeService itemTypeService) : Controller
{
    private readonly IItemService _itemService = itemService;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly IMachineService _machineService = machineService;
    private readonly IItemTypeService _itemTypeService = itemTypeService;

    [HttpGet("Item/List")]
    public async Task<IActionResult> Index()
    {
        var items = await _itemService.ListAsync();
        return View(items);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Item/Create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Machines = await GetMachines();
        ViewBag.ItemTypes = await GetItemTypes();
        return View();
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Item/Create")]
    public async Task<IActionResult> Create(CreateItemViewModel model, List<IFormFile> files)
    {
        var validator = new CreateItemValidator();
        var validationResult = await validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            ViewBag.Machines = await GetMachines();
            ViewBag.ItemTypes = await GetItemTypes();
            validationResult.AddToModelState(this.ModelState);
            return View(model);
        }
        if (files.Count < 2 || files.Count > 2)
        {
            ViewBag.Machines = await GetMachines();
            ViewBag.ItemTypes = await GetItemTypes();
            ModelState.AddModelError("Error", "File must be selected.");
            return View(model);
        }
        var uploadResult = await UploadFile(model, files);
        if (!uploadResult.IsSuccess)
        {
            ViewBag.Machines = await GetMachines();
            ViewBag.ItemTypes = await GetItemTypes();
            ModelState.AddModelError("UploadPath", "Please check your files, if one file must be png or jpg, second file should be solidworks file.");
            return View(model);
        }
        var result = await _itemService.CreateAsync(model);
        if (!result.IsSuccess)
        {
            ViewBag.Machines = await GetMachines();
            ViewBag.ItemTypes = await GetItemTypes();
            ModelState.AddModelError("Error", "Error on adding item");
            return View(model);
        }

        return RedirectToAction("Index", "Item");
    }
    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Item/Delete/{itemId}")]
    public async Task<IActionResult> Delete(int itemId)
    {
        await _itemService.DeleteAsync(itemId);
        return RedirectToAction("Index", "Item");
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Item/{id}/Edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _itemService.GetByIdAsync(id);
        return View(response);
    }
    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Item/{id}/Edit")]
    public async Task<IActionResult> Edit(int id,EditItemViewModel model)
    {
        await _itemService.EditAsync(id, model.Plm);
        return RedirectToAction("Index","Item");
    }


    [HttpGet("Item/{itemImageId}/Download")]
    [Authorize(Roles = "Administrator,Manager,Member")]
    public async Task<IActionResult> Download(int itemImageId)
    {
        var filePath = await _itemService.DownloadAsync(itemImageId);
        var file = Path.GetFileName(filePath.Value);
        var contentType = GetContentType(filePath);
        return File(filePath, contentType, file);
    }
    public static string GetContentType(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        PropertyInfo contentTypeProperty = fileInfo.GetType().GetProperty("ContentType", BindingFlags.Public | BindingFlags.Instance);

        return contentTypeProperty != null ? contentTypeProperty.GetValue(fileInfo).ToString() : "application/octet-stream";
    }
    private async Task<Result> UploadFile(CreateItemViewModel model, List<IFormFile> files)
    {
        bool isAvatar = false;
        bool isProdFile = false;
        if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\images\\")) ;
        {
            Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\images\\");
        }
        foreach (var file in files)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "\\images\\" + file.FileName);
            var extension = Path.GetExtension(filePath);
            if (extension == ".png" || extension == ".jpg")
            {
                isAvatar = true;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    model.Avatar = "\\images\\" + file.FileName;
                }
            }
            else
            {
                isProdFile = true;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    model.UploadPath = "\\images\\" + file.FileName;
                }
            }
        }
        if (isProdFile && isAvatar) return Result.Success();
        return Result.Error();
    }
    private async Task<List<SelectListItem>> GetMachines()
    {
        var items = new List<SelectListItem>();
        var machines = await _machineService.ListAsync();
        foreach (var machine in machines)
        {
            items.Add(new SelectListItem(machine.Name, machine.Id.ToString()));
        }


        return items;
    }
    private async Task<List<SelectListItem>> GetItemTypes()
    {
        var items = new List<SelectListItem>();
        var itemTypes = await _itemTypeService.ListAsync();
        foreach (var itemType in itemTypes)
        {
            items.Add(new SelectListItem(itemType.Name, itemType.Id.ToString()));
        }

        return items;
    }
}