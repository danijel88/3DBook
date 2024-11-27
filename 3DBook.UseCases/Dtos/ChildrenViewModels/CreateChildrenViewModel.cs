namespace _3DBook.UseCases.Dtos.ChildrenViewModels;

public class CreateChildrenViewModel : BaseChildrenViewModel
{
    public int ElasticSize { get; set; }
    public int FolderId { get; set; }
    public string? UploadPath { get; set; }
    public string Avatar { get; set; }
}