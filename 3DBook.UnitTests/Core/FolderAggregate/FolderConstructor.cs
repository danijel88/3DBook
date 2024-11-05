using _3DBook.Core.FolderAggregate;

namespace _3DBook.UnitTests.Core.FolderAggregate;

public class FolderConstructor
{
   
    private int _folds = 2;
    private decimal _enter = 52;
    private decimal _exit = 16;
    private int _machineId = 1;
    private string _machineSortCode = "ZZ";
    private string code = "2_52_16_ZZ";
    private Folder? _folder;
    private Folder CreateFolder()
    {
        return new Folder(_folds, _enter, _exit, _machineId, _machineSortCode);
    }

    [Fact]
    public void InitializeFolderCode()
    {
        _folder = CreateFolder();
        Assert.Equal(code,_folder.Code);
    }

}