using _3DBook.UseCases.Dtos.ItemTypeViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.ItemAggregate;

public interface IItemTypeService
{
    Task<Result> CreateAsync(CreateItemTypeViewModel model);
    Task<List<ItemTypesViewModel>> ListAsync();
}