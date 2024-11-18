using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.Models.ChildrenViewModels;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace _3DBook.UseCases.FolderAggregate;

public class ChildrenService(IRepository<Child> childRepository, ILogger<ChildrenService> logger, IRepository<ChildImage> childImageRepository) : IChildrenService
{
    private readonly IRepository<Child> _childRepository = childRepository;
    private readonly IRepository<ChildImage> _childImageRepository = childImageRepository;
    private readonly ILogger<ChildrenService> _logger = logger;

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
        }).ToList();
    }

    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateChildrenViewModel createViewModel)
    {
        _logger.LogInformation($"Creating child with parameters: Thickness - {createViewModel.Thickness}, MountWidth - {createViewModel.MouthWidth}, MountLength - {createViewModel.MouthLength}");
        var child = new Child(createViewModel.ElasticSize, createViewModel.FolderId, createViewModel.MouthLength,
            createViewModel.MouthWidth, createViewModel.Thickness, createViewModel.Plm);
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
}