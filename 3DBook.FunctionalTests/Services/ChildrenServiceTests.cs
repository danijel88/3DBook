using _3DBook.Core.FolderAggregate;
using _3DBook.Core.FolderAggregate.Specifications;
using _3DBook.Models.ChildrenViewModels;
using _3DBook.UnitTests.Core.Helpers;
using _3DBook.UseCases.FolderAggregate;
using Ardalis.Result;
using Ardalis.SharedKernel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace _3DBook.FunctionalTests.Services;

[UseCulture("en-US")]
public class ChildrenServiceTests
{
    private readonly IChildrenService _childrenService;
    private readonly Mock<ILogger<ChildrenService>> _logger;
    private readonly Mock<IRepository<Child>> _repositoryMock;
    private readonly Mock<IRepository<ChildImage>> _childImageRepoMock;
    private readonly Mock<IChildrenService> _mockingChildrenService;
    private readonly Mock<IWebHostEnvironment> _webHostEnvMock;

    public ChildrenServiceTests()
    {
        _logger = new Mock<ILogger<ChildrenService>>();
        _repositoryMock = new Mock<IRepository<Child>>();
        _childImageRepoMock = new Mock<IRepository<ChildImage>>();
        _mockingChildrenService = new Mock<IChildrenService>();
        _webHostEnvMock = new Mock<IWebHostEnvironment>();
        _childrenService = new ChildrenService(_repositoryMock.Object, _logger.Object, _childImageRepoMock.Object, _webHostEnvMock.Object);
    }

    [Fact]
    public async Task ListAsync_ReturnsChildrenViewModel()
    {
        var children = GetSampleData();

        _repositoryMock.Setup(x => x.ListAsync(It.IsAny<ChildrenWithFolderSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(children);

        var result = await _childrenService.ListAsync(1);

        Assert.NotNull(result);
        Assert.Equal("T2.5_Mw7_Ml5_E1", result.FirstOrDefault()!.Code);
    }

    [Fact]
    public async Task CreateAsync_WhenSaveSuccessfully_ReturnsSuccess()
    {
        _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Child>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Child(1, 1, 5, 2, 2.5m, null));
        _childImageRepoMock.Setup(x => x.AddAsync(new ChildImage("D:\\Upload\\filename.ext", 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ChildImage("D:\\Upload", 1));

        _repositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
        var createViewModel = new CreateChildrenViewModel
        {
            FolderId = 1,
            MouthLength = 5,
            MouthWidth = 2,
            Plm = null,
            Thickness = 2.5m,
            ElasticSize = 1,
            UploadPath = @"D:\\Upload\\filename.ext"
        };

        var result = await _childrenService.CreateAsync(createViewModel);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Edit_WhenUpdateSaveSuccessfully_ReturnsSuccess()
    {// Arrange
        int childId = 1;
        string oldPlm = "OldPLM";
        string newPlm = "NewPLM";

        var child = new Child(10, 1, 20, 30, 0.5m, oldPlm);
        _repositoryMock
            .Setup(x => x.GetByIdAsync(childId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(child);

        _repositoryMock.
            Setup(x => x.UpdateAsync(It.IsAny<Child>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _repositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);


        var result = await _childrenService.Edit(1, newPlm);

        Assert.True(result.IsSuccess);
        Assert.Equal(newPlm, child.Plm);
    }

    [UseCulture("en-US")]
    public List<Child> GetSampleData()
    {
        var children = new List<Child>()
        {
            new Child(1, 1, 5, 7, 2.5m, null,new Folder(4,10,15,1,"ZZ"),new ChildImage(@"D:\\",1)),
            new Child(5, 1, 10, 7, 2.5m, "A356",new Folder(4,10,15,1,"ZZ"),new ChildImage(@"D:\\",2)),
        };
        return children;
    }
}