using _3DBook.Core.ItemAggregate;
using _3DBook.Infrastructure.Data.Config;
using _3DBook.Models.ItemViewModel;
using FluentValidation;

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