using _3DBook.UseCases.Dtos.ChildrenViewModels;
using FluentValidation;

namespace _3DBook.Validators.FolderAggregate.Validators;

public class CreateChildrenValidator : AbstractValidator<CreateChildrenViewModel>
{
    public CreateChildrenValidator()
    {
        RuleFor(x => x.ElasticSize)
            .GreaterThan(-1);
        RuleFor(x => x.MouthLength)
            .GreaterThan(-1);
        RuleFor(x => x.MouthWidth)
            .GreaterThan(-1);
        RuleFor(x => x.Thickness)
            .GreaterThan(-1);
        RuleFor(x => x.FolderId)
            .GreaterThan(0);
    }
}