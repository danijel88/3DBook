using _3DBook.Models.AccountViewModel;
using FluentValidation;

namespace _3DBook.Validators.AccountsAggregate.Validators;

public class CreateAccountValidator : AbstractValidator<CreateAccountViewModel>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(256).WithMessage("Email length exceeded.");
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("First name length exceeded");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Last name length exceeded");
        RuleFor(x => x.Company)
            .NotEmpty()
            .MaximumLength(450)
            .WithMessage("Company length exceeded");
        RuleFor(x => x.RoleName)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Role name length exceeded");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(1024)
            .WithMessage("Password length exceeded");
    }
}