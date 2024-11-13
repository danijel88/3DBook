using _3DBook.Models.FolderViewModel;
using FluentValidation;

namespace _3DBook.UseCases.FolderAggregate.Validators;

public class CreateFolderValidator : AbstractValidator<CreateFolderViewModel>
{
    public CreateFolderValidator()
    {
        RuleFor(x => x.MachineId)
            .GreaterThan(0);
        RuleFor(x => x.Enter)
            .GreaterThan(0);
        RuleFor(x => x.Exit)
            .GreaterThan(0);
        RuleFor(x => x.Folds)
            .GreaterThan(-1);
    }
}