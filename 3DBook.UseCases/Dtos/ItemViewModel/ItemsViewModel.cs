namespace _3DBook.UseCases.Dtos.ItemViewModel;

public class ItemsViewModel : BaseItemViewModel
{
    public int Id { get; set; }

    public string Machine { get; set; }
    public string ItemType { get; set; }
    public string ImagePath { get; set; }
    public string? Plm { get; set; }
    public int ItemImageId { get; set; }
}