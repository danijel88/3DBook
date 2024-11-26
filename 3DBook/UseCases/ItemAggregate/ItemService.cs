using _3DBook.Core.ItemAggregate;
using _3DBook.Core.ItemAggregate.Specifications;
using _3DBook.Models.ItemViewModel;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.AspNetCore.Hosting;

namespace _3DBook.UseCases.ItemAggregate;

public class ItemService(IRepository<Item> repository,
    IRepository<ItemImage> itemImageRepository, 
    ILogger<ItemService> logger,
    IWebHostEnvironment webHostEnvironment) : IItemService
{
    private readonly IRepository<Item> _repository = repository;
    private readonly ILogger<ItemService> _logger = logger;
    private readonly IRepository<ItemImage> _itemImageRepository = itemImageRepository;
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    public async Task<List<ItemsViewModel>> ListAsync()
    {
        var spec = new GetItemsWithTypesAndMachinesSpec();
        var response = await _repository.ListAsync(spec);
        return response
            .Select(s => new ItemsViewModel
            {
                Code = s.Code,
                Avatar = s.Avatar,
                Id = s.Id,
                ItemType = s.ItemType.Name,
                Machine = s.Machine.Name,
                ImagePath = s.ItemImage.Path,
                ItemImageId = s.ItemImage.Id,
                Plm = s.Plm
            }).ToList();
    }

    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateItemViewModel model)
    {
        _logger.LogInformation($"Creating item {model.Code}");
       var newItem = await _repository.AddAsync(new Item(model.MachineId,null ,model.Code, model.ItemTypeId, model.Avatar));
        var result = await _repository.SaveChangesAsync();
        await AddItemImageAsync(model.UploadPath, newItem.Id);
        return result == 0 ? Result.Success() : Result.Error("Failed to save");
    }

    /// <inheritdoc />
    public async Task<Result> DeleteAsync(int itemId)
    {
        _logger.LogInformation($"Deleting itemId: {itemId}");
        var item = await _repository.GetByIdAsync(itemId);
        var spec = new GetItemImagesByItemIdSpec(itemId);
        var itemImages = await _itemImageRepository.ListAsync(spec);
        foreach (var image in itemImages)
        {
            _logger.LogInformation($"Deleting files: {_webHostEnvironment}{image.Path}");
            File.Delete(_webHostEnvironment.WebRootPath + image.Path);
        }
        _logger.LogInformation("Saving deletion.");
        await _itemImageRepository.DeleteRangeAsync(itemImages);
        await _itemImageRepository.SaveChangesAsync();
        await _repository.DeleteAsync(item);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Saved.");
        return Result.Success();
    }

    /// <inheritdoc />
    public async Task<Result<string>> DownloadAsync(int id)
    {
        var itemImage = await _itemImageRepository.GetByIdAsync(id);
        if (itemImage == null)
        {
            return Result.NotFound();
        }

        return itemImage.Path;
    }

    /// <inheritdoc />
    public async Task<EditItemViewModel> GetByIdAsync(int id)
    {
        var response = new EditItemViewModel();
        var item = await _repository.GetByIdAsync(id);
        if (item is null) return response;
        response.Plm = item.Plm;
        return response;
    }

    /// <inheritdoc />
    public async Task<Result> EditAsync(int id, string? plm)
    {
        var item = await _repository.GetByIdAsync(id);
        item.UpdatePlm(plm);
        await _repository.UpdateAsync(item);
        await _repository.SaveChangesAsync();
        return Result.Success();
    }

    private async Task<Result> AddItemImageAsync(string path, int itemId)
    {
        await _itemImageRepository.AddAsync(new ItemImage(path, itemId));
        var result = await _itemImageRepository.SaveChangesAsync();
        return result == 0 ? Result.Success() : Result.Error("Failed to save");
    }
}