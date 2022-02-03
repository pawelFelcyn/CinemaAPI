using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RulesForEmail();
        RulesForPassword();
    }

    private void RulesForEmail()
    {
        RuleFor(l => l.Email)
            .NotNull()
            .WithMessage("Email must not be null")
            .NotEmpty()
            .WithMessage("Email must not be empty")
            .EmailAddress()
            .WithMessage("This is not a valid email address");
    }

    private void RulesForPassword()
    {
        RuleFor(l => l.Password)
            .NotNull()
            .WithMessage("Password must not be null")
            .NotEmpty()
            .WithMessage("Password must not be empty");
    }
}
