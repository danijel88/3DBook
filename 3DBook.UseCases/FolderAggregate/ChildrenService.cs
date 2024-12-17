using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.UseCases.Dtos.ChildrenViewModels;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace _3DBook.UseCases.FolderAggregate;

public class ChildrenService(IRepository<Child> childRepository,
    ILogger<ChildrenService> logger,
    IRepository<ChildImage> childImageRepository,
    IWebHostEnvironment webHostEnvironment) : IChildrenService
{
    private readonly IRepository<Child> _childRepository = childRepository;
    private readonly IRepository<ChildImage> _childImageRepository = childImageRepository;
    private readonly ILogger<ChildrenService> _logger = logger;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    /// <param name="folderId"></param>
    /// <inheritdoc />
    public async Task<List<ChildrenViewModel>> ListAsync(int folderId)
    {
        _logger.LogInformation($"Getting children for folderId: {folderId}.");
        var spec = new ChildrenWithFolderSpec(folderId);
        var children = await _childRepository.ListAsync(spec);
        if (children is null) return new List<ChildrenViewModel>();
        return children.Select(s => new ChildrenViewModel
        {
            Code = s.Code,
            FatherCode = s.Folder.Code,
            Id = s.Id,
            MouthLength = s.MouthLength,
            MouthWidth = s.MouthWidth,
            Plm = s.Plm,
            Thickness = s.Thickness,
            Path = s.ChildImage.Path,
            ChildImageId = s.ChildImage.Id,
            Avatar = s.Avatar
        }).ToList();
    }

    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateChildrenViewModel createViewModel)
    {
        _logger.LogInformation($"Creating child with parameters: Thickness - {createViewModel.Thickness}, MountWidth - {createViewModel.MouthWidth}, MountLength - {createViewModel.MouthLength}");
        var child = new Child(createViewModel.ElasticSize, createViewModel.FolderId, createViewModel.MouthLength,
            createViewModel.MouthWidth, createViewModel.Thickness, createViewModel.Plm,createViewModel.Avatar);
        var newChild = await _childRepository.AddAsync(child);
        var result = await _childRepository.SaveChangesAsync();

        await AddChildImage(createViewModel.UploadPath, newChild.Id);
        return result == 0 ? Result.Success() : Result.Error("Failed to save");
    }


    /// <inheritdoc />
    public async Task<Result> AddChildImage(string path, int childId)
    {
        await _childImageRepository.AddAsync(new ChildImage(path, childId));
        await _childImageRepository.SaveChangesAsync();
        return Result.Success();
    }

    /// <inheritdoc />
    public async Task<Result<string>> DownloadAsync(int id)
    {
        var childImage = await _childImageRepository.GetByIdAsync(id);
        if (childImage == null)
        {
            return Result.NotFound();
        }

        return childImage.Path;
    }

    /// <inheritdoc />
    public async Task<Result> DeleteAsync(int childId)
    {
        _logger.LogInformation("Deleting childId: {childId}",childId);
        var child = await _childRepository.GetByIdAsync(childId);
        var childImagesSpec = new ChildImagesByChildSpec(childId);
        var childImages = await _childImageRepository.ListAsync(childImagesSpec);
        foreach (var image in childImages)
        {
            _logger.LogInformation($"Deleting files: {_webHostEnvironment}{image.Path}");
            File.Delete(_webHostEnvironment.WebRootPath + image.Path);
        }
        _logger.LogInformation("Saving deletion.");
        await _childImageRepository.DeleteRangeAsync(childImages);
        await _childRepository.DeleteAsync(child);
        await _childRepository.SaveChangesAsync();
        await _childImageRepository.SaveChangesAsync();
        _logger.LogInformation("Saved.");
        return Result.Success();
        
    }

    /// <inheritdoc />
    public async Task<Result> Edit(int childId, EditChildrenViewModel model)
    {
        _logger.LogInformation("Editing childId: {childId}",childId);
        var child = await _childRepository.GetByIdAsync(childId);
        _logger.LogInformation($"Old Plm: {child.Plm} new value {model}");
        child.UpdateChild(model.ElasticSize,model.MouthLength,model.MouthWidth,model.Thickness,model.Plm);
        await _childRepository.UpdateAsync(child);
        await _childRepository.SaveChangesAsync();
        return Result.Success();
        
    }

    /// <inheritdoc />
    public async Task<EditChildrenViewModel> GetByIdAsync(int childId)
    {
        var response = new EditChildrenViewModel();
        var child = await _childRepository.GetByIdAsync(childId);
        if (child is null) return response;
        response.ChildId = child.Id;
        response.Plm = child.Plm;
        response.ElasticSize = child.ElasticSize;
        response.MouthLength = child.MouthLength;
        response.MouthWidth = child.MouthWidth;
        response.Thickness = child.Thickness;
        return response;
    }
}