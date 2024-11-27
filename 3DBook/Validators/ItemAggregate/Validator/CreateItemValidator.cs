using _3DBook.Infrastructure.Data.Config;
using _3DBook.UseCases.Dtos.ItemViewModel;
using FluentValidation;

namespace _3DBook.Validators.ItemAggregate.Validator;

public class CreateItemValidator : AbstractValidator<CreateItemViewModel>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .NotNull()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
    }
}