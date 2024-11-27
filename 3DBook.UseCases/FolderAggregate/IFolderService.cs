using _3DBook.UseCases.Dtos.FolderViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.FolderAggregate;

public interface IFolderService
{
    public Task<List<FoldersViewModel>> ListAsync();
    public Task<Result> CreateAsync(CreateFolderViewModel model);
}