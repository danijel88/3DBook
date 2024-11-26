using _3DBook.Core.ItemAggregate;
using _3DBook.Core.ItemAggregate.Specifications;
using _3DBook.Models.ItemTypeViewModel;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace _3DBook.UseCases.ItemAggregate;

public class ItemTypeService(IRepository<ItemType> repository,ILogger<ItemTypeService> logger) : IItemTypeService
{
    private readonly IRepository<ItemType> _repository = repository;
    private readonly ILogger<ItemTypeService> _logger = logger;

    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateItemTypeViewModel model)
    {
        _logger.LogInformation($"Creating item type: {model.Name}");
        if (await DoesExistItemType(model.Name))
        {
            _logger.LogError($"Item type exist {model.Name}");
            return Result.Error("Item Type exist.");
        }

        await _repository.AddAsync(new ItemType(model.Name));
        var result = await _repository.SaveChangesAsync();
        return result == 0 ? Result.Success() : Result.Error("Error on saving.");
    }

    /// <inheritdoc />
    public async Task<List<ItemTypesViewModel>> ListAsync()
    {
        var itemTypes = await _repository.ListAsync();
        
        return itemTypes.Select(s => new ItemTypesViewModel
        {
            Id = s.Id,
            Name = s.Name
        }).ToList();
    }

    private async Task<bool> DoesExistItemType(string name)
    {
        var spec = new GetItemTypeByNameSpec(name);
        return await _repository.AnyAsync(spec);
    }
}