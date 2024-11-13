using System.ComponentModel;

namespace _3DBook.Models.FolderViewModel;

public class FoldersViewModel : BaseFolderViewModel
{
    public int Id { get; set; }
    public string MachineName { get; set; }
    public string Code { get; set; }
}