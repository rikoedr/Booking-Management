﻿using API.Data;
using API.DataTransferObjects.Creates;
using API.Utilities.Responses;
using FluentValidation;

namespace API.Utilities.Validations;

public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDTO>
{
    public CreateAccountRoleValidator()
    {
        RuleFor(ar => ar.AccountGuid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);

        RuleFor(ar => ar.RoleGuid)
            .NotEmpty().WithMessage(Messages.CanNotBeEmpty);
    }
}
