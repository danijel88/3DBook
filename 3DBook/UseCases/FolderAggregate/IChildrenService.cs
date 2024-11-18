using _3DBook.Models.ChildrenViewModels;
using Ardalis.Result;

namespace _3DBook.UseCases.FolderAggregate;

public interface IChildrenService
{
    Task<List<ChildrenViewModel>> ListAsync(int folderId);
    Task<Result> CreateAsync(CreateChildrenViewModel createViewModel);
    Task<Result> AddChildImage(string path, int childId);
    Task<Result<string>> DownloadAsync(int id);
    Task<Result> DeleteAsync(int childId);
}