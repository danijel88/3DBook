using System.Net.Mime;
using System.Reflection;
using _3DBook.Models.ChildrenViewModels;
using _3DBook.UseCases.FolderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace _3DBook.Controllers;

[Authorize(Roles = "Administrator,Manager,Member,Operator")]
public class ChildrenController(IChildrenService childrenService, IWebHostEnvironment webHostEnvironment) : Controller
{
    private readonly IChildrenService _childrenService = childrenService;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [HttpGet("Children/{id}/List")]
    public async Task<IActionResult> List(int id)
    {
        var children = await _childrenService.ListAsync(id);
        ViewBag.FolderId = id;
        return View(children);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Children/{id}/Create")]
    public IActionResult Create(int id)
    {
        return View();
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Children/{id}/Create")]
    public async Task<IActionResult> Create(int id, CreateChildrenViewModel model, IFormFile file)
    {
        if (file.Length == 0)
        {
            ModelState.AddModelError("Error", "File must be selected.");
            return View(model);
        }

        await UploadFile(id, model, file);

        var result = await _childrenService.CreateAsync(model);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", "Error on adding child");
            return View(model);
        }

        return RedirectToAction("List", new { id = id });
    }


    [HttpGet("Children/{id}/Download")]
    public async Task<IActionResult> Download(int id)
    {
        var filePath = await _childrenService.Download(id);
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

    private async Task UploadFile(int id, CreateChildrenViewModel model, IFormFile file)
    {
        if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\images\\")) ;
        {
            Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\images\\");
        }

        var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "\\images\\" + file.FileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
            model.UploadPath = "\\images\\" + file.FileName;
            model.FolderId = id;
        }
    }
}