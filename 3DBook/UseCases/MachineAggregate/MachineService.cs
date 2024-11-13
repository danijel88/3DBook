using _3DBook.Core.MachineAggregate;
using _3DBook.Infrastructure;
using _3DBook.Models.MachineViewModel;
using Ardalis.Result;
using Ardalis.SharedKernel;

namespace _3DBook.UseCases.MachineAggregate;

public class MachineService(IRepository<Machine> machineRepository, ILogger<MachineService> logger) : IMachineService
{
    private readonly IRepository<Machine> _machineRepository = machineRepository;
    private readonly ILogger<MachineService> _logger = logger;
    /// <inheritdoc />
    public async Task<Result> CreateAsync(CreateMachineViewModel model)
    {
        _logger.LogInformation($"Creating machine {model.Name}.");
        await _machineRepository.AddAsync(new Machine(model.Name, model.SortCode));
        var result = await _machineRepository.SaveChangesAsync();
        return result == 0 ? Result.Success() : Result.Error("Error during the saving");
    }

    /// <inheritdoc />
    public async Task<IList<MachinesViewModel>> ListAsync()
    {
        var machines = await _machineRepository.ListAsync();
        return machines.Select(s => new MachinesViewModel
        {
            Name = s.Name,
            SortCode = s.SortCode
        }).ToList();
    }
}