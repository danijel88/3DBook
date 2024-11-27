namespace _3DBook.UseCases.Dtos.ItemViewModel;

public class CreateItemViewModel : BaseItemViewModel
{
    public int MachineId { get; set; }
    public int ItemTypeId { get; set; }
    public string UploadPath { get; set; }
    
}