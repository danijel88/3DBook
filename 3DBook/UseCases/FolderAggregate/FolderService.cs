using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.Models.FolderViewModel;
using Ardalis.SharedKernel;

namespace _3DBook.UseCases.FolderAggregate;

public class FolderService(IRepository<Folder> folderRepository,ILogger<FolderService> logger) : IFolderService
{
    private readonly IRepository<Folder> _folderRepository  = folderRepository;
    private readonly ILogger<FolderService> _logger = logger;

    /// <inheritdoc />
    public async Task<List<FoldersViewModel>> ListAsync()
    {
        _logger.LogInformation("Call folders list");
        var spec = new FoldersWithMachinesSpec();
        var response = await _folderRepository.ListAsync(spec);
        return response.Select(s => new FoldersViewModel
        {
            Code = s.Code,
            Enter = s.Enter,
            MachineName = s.Machine.Name,
            Exit = s.Exit,
            Folds = s.Folds,
        }).ToList();
    }
}