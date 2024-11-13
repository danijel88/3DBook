using _3DBook.Models.FolderViewModel;

namespace _3DBook.UseCases.FolderAggregate;

public interface IFolderService
{
    public Task<List<FoldersViewModel>> ListAsync();
}