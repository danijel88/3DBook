namespace _3DBook.UseCases.Dtos.ChildrenViewModels;

public class ChildrenViewModel : BaseChildrenViewModel
{
    public string Code { get; set; }
    public string FatherCode { get; set; }
    public int Id { get; set; }
    public string Path { get; set; }
    public int ChildImageId { get; set; }
    public string Avatar { get; set; }
}