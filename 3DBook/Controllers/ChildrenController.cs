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

    [HttpGet("Children/{folderId}/List")]
    public async Task<IActionResult> List(int folderId)
    {
        var children = await _childrenService.ListAsync(folderId);
        ViewBag.FolderId = folderId;
        return View(children);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Children/{folderId}/Create")]
    public IActionResult Create(int folderId)
    {
        return View();
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Children/{folderId}/Create")]
    public async Task<IActionResult> Create(int folderId, CreateChildrenViewModel model, IFormFile file)
    {
        if (file.Length == 0)
        {
            ModelState.AddModelError("Error", "File must be selected.");
            return View(model);
        }

        await UploadFile(folderId, model, file);

        var result = await _childrenService.CreateAsync(model);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", "Error on adding child");
            return View(model);
        }

        return RedirectToAction("List", new { folderId = folderId });
    }


    [HttpGet("Children/{childId}/Download")]
    public async Task<IActionResult> Download(int childId)
    {
        var filePath = await _childrenService.DownloadAsync(childId);
        var file = Path.GetFileName(filePath.Value);
        var contentType = GetContentType(filePath);
        return File(filePath, contentType, file);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Children/{folderId}/Delete/{childId}")]
    public async Task<IActionResult> Delete(int childId,int folderId)
    {
        await _childrenService.DeleteAsync(childId);
        return RedirectToAction("List", "Children", new { folderId = folderId });
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