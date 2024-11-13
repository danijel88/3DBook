﻿using _3DBook.Infrastructure.Data.Config;
using _3DBook.Models.MachineViewModel;
using FluentValidation;

namespace _3DBook.UseCases.MachineAggregate.Validators;

public class CreateMachineValidator : AbstractValidator<CreateMachineViewModel>
{
    public CreateMachineValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        RuleFor(x => x.SortCode)
            .NotEmpty()
            .NotNull()
            .MaximumLength(DataSchemaConstants.DEFAULT_SHORT_NAME_LENGTH);
    }
}