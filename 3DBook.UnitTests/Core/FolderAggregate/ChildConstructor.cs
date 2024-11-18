using _3DBook.Core.FolderAggregate;
using _3DBook.UnitTests.Core.Helpers;

namespace _3DBook.UnitTests.Core.FolderAggregate;

[UseCulture("en-US")]
public class ChildConstructor
{
    private decimal _thickness = 1.0m;
    private int _mouthWidth = 4;
    private int _mouthLength = 5;
    private int _elasticSize = 0;
    private string _code = "T1.0_Mw4_Ml5_E0";
    private int _folderId = 1;
    private string _plm = null;
    private Child? _child;

    private Child CreateChild()
    {
        return new Child(_elasticSize, _folderId, _mouthLength, _mouthWidth, _thickness,_plm);
    }

    [Fact]
    public void InitializeNewChild()
    {
        _child = CreateChild();
        Assert.Equal(_code,_child.Code);
    }
}