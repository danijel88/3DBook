using _3DBook.UseCases.Dtos.ChildrenViewModels;
using Ardalis.Result;

namespace _3DBook.UseCases.FolderAggregate;

public interface IChildrenService
{
    Task<List<ChildrenViewModel>> ListAsync(int folderId);
    Task<Result> CreateAsync(CreateChildrenViewModel createViewModel);
    Task<Result> AddChildImage(string path, int childId);
    Task<Result<string>> DownloadAsync(int id);
    Task<Result> DeleteAsync(int childId);
    Task<Result> Edit(int childId, EditChildrenViewModel model);
    Task<EditChildrenViewModel> GetByIdAsync(int childId);
}