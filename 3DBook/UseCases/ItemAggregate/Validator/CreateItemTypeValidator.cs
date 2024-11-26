using _3DBook.Infrastructure.Data.Config;
using _3DBook.Models.ItemTypeViewModel;
using FluentValidation;

namespace _3DBook.UseCases.ItemAggregate.Validator;

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