using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.Core.MachineAggregate;
using _3DBook.UseCases.Dtos.FolderViewModel;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;

namespace _3DBook.UseCases.FolderAggregate;

public class FolderService(IRepository<Folder> folderRepository, ILogger<FolderService> logger, IRepository<Machine> machineRepository) : IFolderService
{
    private readonly IRepository<Machine> _machineRepository = machineRepository;
    private readonly IRepository<Folder> _folderRepository = folderRepository;
    private readonly ILogger<FolderService> _logger = logger;

    /// <inheritdoc />
    public async Task<List<FoldersViewModel>> ListAsync()
    {
        _logger.LogInformation("Call folders list");
        var spec = new FoldersWithMachinesSpec();
        var response = await _folderRepository.ListAsync(spec);
        return response.Select(s => new FoldersViewModel
        {
            Id = s.Id,
            Code = s.Code,
            Enter = s.Enter,
            MachineName = s.Machine.Name,
            Exit = s.Exit,
            Folds = s.Folds,
        }).ToList();
    }

    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateFolderViewModel model)
    {
        _logger.LogInformation($"Creating folder with parameters, Enter: {model.Enter}, Exit: {model.Exit}, Folds: {model.Folds}, MachineId: {model.MachineId} ");
        var machine = await _machineRepository.GetByIdAsync(model.MachineId);
        if (machine is null)
        {
            _logger.LogError($"Machine with Id:{model.MachineId} does not exist");
            return Result.Error("Machine does not exist.");
        }
        var folder = new Folder(model.Folds, model.Enter, model.Exit, model.MachineId, machine!.SortCode);
        await _folderRepository.AddAsync(folder);
        var result = await _folderRepository.SaveChangesAsync();
        if (result != 0)
        {
            _logger.LogError("Error on saving folder");
            return Result.Error("Error on saving");
        }
        return Result.Success();
    }
}