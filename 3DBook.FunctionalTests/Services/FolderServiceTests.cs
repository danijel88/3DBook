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

    public FolderServiceTests()
    {
        _logger = new Mock<ILogger<FolderService>>();
        _repositoryMock = new Mock<IRepository<Folder>>();
        _folderService = new FolderService(_repositoryMock.Object, _logger.Object);
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