using _3DBook.Core.FolderAggregate;

namespace _3DBook.UnitTests.Core.FolderAggregate;

public class ChildImagesConstructor
{
    private string _path = "/images/1.jpg";
    private int _childId = 1;
    private ChildImage? _childImages;

    private ChildImage CreateNewChildImages()
    {
        return new ChildImage(_path, _childId);
    }

    [Fact]
    public void InitializeChildImages()
    {
        _childImages = CreateNewChildImages();
        Assert.Equal(_path,_childImages.Path);
        Assert.Equal(_childId,_childImages.ChildId);
    }
}