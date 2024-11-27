using _3DBook.UseCases.Dtos.MachineViewModel;
using Ardalis.Result;

namespace _3DBook.UseCases.MachineAggregate;

public interface IMachineService
{
    Task<Result> CreateAsync(CreateMachineViewModel model);
    Task<IList<MachinesViewModel>> ListAsync();
}