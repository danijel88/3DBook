using _3DBook.Models.ItemTypeViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.ItemAggregate;

public interface IItemTypeService
{
    Task<Result> CreateAsync(CreateItemTypeViewModel model);
    Task<List<ItemTypesViewModel>> ListAsync();
}