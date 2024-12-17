using System.Net.Mime;
using System.Reflection;
using _3DBook.UseCases.Dtos.ChildrenViewModels;
using _3DBook.UseCases.FolderAggregate;
using _3DBook.Validators.FolderAggregate.Validators;
using Ardalis.Result;
using FluentValidation.AspNetCore;
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
    public async Task<IActionResult> Create(int folderId, CreateChildrenViewModel model, List<IFormFile> files)
    {
        var validator = new CreateChildrenValidator();
        var validationResult = await validator.ValidateAsync(model);
       
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View(model);
        }

        if (files.Count < 2)
        {
            ModelState.AddModelError("UploadPath", "We must have 2 files, one file must be with extension jpg or png, second file is for downloading 3d part.");
            return View(model);
        }


        var uploadResult = await UploadFile(folderId, model, files);
        if (!uploadResult.IsSuccess)
        {
            ModelState.AddModelError("UploadPath", "Please check your files, if one file must be png or jpg, second file should be solidworks file.");
            return View(model);
        }

        var result = await _childrenService.CreateAsync(model);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("Error", "Error on adding child");
            return View(model);
        }

        return RedirectToAction("List", new { folderId = folderId });
    }


    [HttpGet("Children/{childId}/Download")]
    [Authorize(Roles = "Administrator,Manager,Member")]
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

    [Authorize(Roles = "Administrator,Manager")]
    [HttpGet("Children/{folderId}/Edit/{childId}")]
    public async Task<IActionResult> Edit(int folderId, int childId)
    {
        var response = await _childrenService.GetByIdAsync(childId);
        return View(response);
    }

    [Authorize(Roles = "Administrator,Manager")]
    [HttpPost("Children/{folderId}/Edit/{childId}")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Edit(int folderId, int childId, EditChildrenViewModel model)
    {
        await _childrenService.Edit(childId, model);
        return RedirectToAction("List", new { folderId = folderId });
    }
    public static string GetContentType(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        PropertyInfo contentTypeProperty = fileInfo.GetType().GetProperty("ContentType", BindingFlags.Public | BindingFlags.Instance);

        return contentTypeProperty != null ? contentTypeProperty.GetValue(fileInfo).ToString() : "application/octet-stream";
    }

    private async Task<Result> UploadFile(int id, CreateChildrenViewModel model, List<IFormFile> files)
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
                    model.Avatar = "\\images\\"+file.FileName;
                }
            }
            else
            {
                isProdFile = true;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    model.UploadPath = "\\images\\" + file.FileName;
                    model.FolderId = id;
                }
            }
        }
        if (isProdFile && isAvatar) return Result.Success();
        return Result.Error();
    }
}