using _3DBook.UseCases.Dtos.ItemViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.ItemAggregate;

public interface IItemService
{
    Task<List<ItemsViewModel>> ListAsync();
    Task<Result> CreateAsync(CreateItemViewModel model);
    Task<Result> DeleteAsync(int itemId);
    Task<Result<string>> DownloadAsync(int itemId);
    Task<EditItemViewModel> GetByIdAsync(int id);
    Task<Result> EditAsync(int id, EditItemViewModel model);
}