using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.Core.MachineAggregate;
using _3DBook.Models.FolderViewModel;
using _3DBook.UseCases.FolderAggregate;
using Ardalis.SharedKernel;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;

namespace _3DBook.FunctionalTests.Services;

public class FolderServiceTests
{
    private readonly IFolderService _folderService;
    private readonly Mock<ILogger<FolderService>> _logger;
    private readonly Mock<IRepository<Folder>> _repositoryMock;
    private readonly Mock<IRepository<Machine>> _machineMock;

    public FolderServiceTests()
    {
        _logger = new Mock<ILogger<FolderService>>();
        _repositoryMock = new Mock<IRepository<Folder>>();
        _machineMock = new Mock<IRepository<Machine>>();
        _folderService = new FolderService(_repositoryMock.Object, _logger.Object,_machineMock.Object);
    }

    [Fact]
    public async Task ListAsync_ReturnsFolderViewModel()
    {
        var folders = GetSampleData();

        _repositoryMock.Setup(x => x.ListAsync(It.IsAny<FoldersWithMachinesSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(folders);

        var result = await _folderService.ListAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("4_20_10_ZZ", result.FirstOrDefault()!.Code);
    }

    [Fact]
    public async Task CreateAsync_WhenSaveSuccessful_ReturnsSuccess()
    {
        var viewModel = new CreateFolderViewModel()
        {
            Enter = 20,
            Exit = 15,
            Folds = 1,
            MachineId = 1
        };
        var folder = new Folder(viewModel.Folds, viewModel.Enter, viewModel.Exit, viewModel.MachineId, "DL");
        _machineMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Machine("Double", "DL"));
        _repositoryMock.Setup(s => s.AddAsync(It.IsAny<Folder>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(folder);
        _repositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        var result = await _folderService.CreateAsync(viewModel);

        Assert.True(result.IsSuccess);
        Assert.Equal($"{viewModel.Folds}_{viewModel.Enter}_{viewModel.Exit}_DL",folder.Code);
    }
    [Fact]
    public async Task CreateAsync_WhenSaveNotSuccessful_ReturnsError()
    {
        var viewModel = new CreateFolderViewModel()
        {
            Enter = 20,
            Exit = 15,
            Folds = 1,
            MachineId = 1
        };
        var folder = new Folder(viewModel.Folds, viewModel.Enter, viewModel.Exit, viewModel.MachineId, "DL");
        _machineMock.Setup(s => s.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Machine("Double", "DL"));
        _repositoryMock.Setup(s => s.AddAsync(It.IsAny<Folder>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(folder);
        _repositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var result = await _folderService.CreateAsync(viewModel);

        Assert.False(result.IsSuccess);
    }

    private List<Folder> GetSampleData()
    {
        var folders = new List<Folder>
        {
            new Folder(4,20,10,1,"ZZ",new Machine("Machine1", "ZZ")),
            new Folder(5,25,15,1,"DL",new Machine("Machine2", "DL"))
        };
        return folders;
    }
}