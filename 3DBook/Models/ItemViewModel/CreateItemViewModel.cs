namespace _3DBook.Models.ItemViewModel;

public class CreateItemViewModel : BaseItemViewModel
{
    public int MachineId { get; set; }
    public int ItemTypeId { get; set; }
    public string UploadPath { get; set; }
    
}