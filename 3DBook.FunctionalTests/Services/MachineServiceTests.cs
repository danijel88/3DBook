using _3DBook.Core.MachineAggregate;
using _3DBook.Models.MachineViewModel;
using _3DBook.UseCases.MachineAggregate;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using Moq;

namespace _3DBook.FunctionalTests.Services;

public class MachineServiceTests
{
    private Mock<IRepository<Machine>> _machineRepositoryMock;
    private Mock<ILogger<MachineService>> _loggerMock;
    private IMachineService _machineService;
    public MachineServiceTests()
    {
        _machineRepositoryMock = new Mock<IRepository<Machine>>();

        _loggerMock = new Mock<ILogger<MachineService>>();
        _machineService = new MachineService(_machineRepositoryMock.Object, _loggerMock.Object);
    }
    [Fact]
    public async Task CreateAsync_WhenSaveSuccessful_ReturnsSuccess()
    {
        var model = new CreateMachineViewModel
        {
            Name = "Zig Zag",
            SortCode = "ZZ"
        };
        _machineRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Machine>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Machine(model.Name, model.SortCode));
        _machineRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        var result = await _machineService.CreateAsync(model);

        Assert.True(result.IsSuccess);


    }
    [Fact]
    public async Task CreateAsync_WhenSaveSuccessful_ReturnsError()
    {
        var model = new CreateMachineViewModel
        {
            Name = "Zig Zag",
            SortCode = "ZZ"
        };
        _machineRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Machine>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Machine(model.Name, model.SortCode));
        _machineRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var result = await _machineService.CreateAsync(model);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task ListAsync_ReturnsMachineList()
    {
        var machines = new List<Machine>
        {
            new Machine("Test", "TT"),
            new Machine("Test 1", "TT1"),
            new Machine("Test 2", "TT2"),
        };
        _machineRepositoryMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(machines);

        var result = await _machineService.ListAsync();

        Assert.Equal(3,result.Count);
    }
}