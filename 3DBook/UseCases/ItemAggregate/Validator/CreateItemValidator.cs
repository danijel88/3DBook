using _3DBook.Infrastructure.Data.Config;
using FluentValidation;
using _3DBook.Models.ItemViewModel;


namespace _3DBook.UseCases.ItemAggregate.Validator;

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