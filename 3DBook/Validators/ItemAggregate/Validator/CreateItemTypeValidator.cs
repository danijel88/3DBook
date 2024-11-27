using _3DBook.Infrastructure.Data.Config;
using _3DBook.UseCases.Dtos.ItemTypeViewModel;
using FluentValidation;

namespace _3DBook.Validators.ItemAggregate.Validator;

public class CreateItemTypeValidator : AbstractValidator<CreateItemTypeViewModel>
{
    public CreateItemTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}