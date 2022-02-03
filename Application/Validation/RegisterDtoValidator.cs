using Application.Dtos;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validation;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private readonly IEmailValidationHelper _helper;
    private readonly string[] _allowedRoleName = { "Manager", "User" };
    private const char SEPARATOR = ',';

    public RegisterDtoValidator(IEmailValidationHelper helper)
    {
        _helper = helper;

        RulesForFirstName();
        RulesForLastName();
        RulesForEmail();
        RulesForRoleName();
        RulesForBirthDate();
        RulesForPassword();
        RulesForConfirmPassword();
    }

    private void RulesForFirstName()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .WithMessage("FirstName must not be empty")
            .NotNull()
            .WithMessage("FirstName must not be null")
            .MaximumLength(20)
            .WithMessage("The maximux length of FirstName is 20 characters");
    }

    private void RulesForLastName()
    {
        RuleFor(r => r.LastName)
            .NotEmpty()
            .WithMessage("LastName must not be empty")
            .NotNull()
            .WithMessage("LastName must not be null")
            .MaximumLength(20)
            .WithMessage("The maximux length of LastName is 20 characters");
    }

    private void RulesForEmail()
    {
        RuleFor(r => r.Email)
            .NotNull()
            .WithMessage("Email must not be null")
            .NotEmpty()
            .WithMessage("Email must not be empty")
            .EmailAddress()
            .WithMessage("This is not a valid email format")
            .Must(e => !_helper.IsTaken(e))
            .WithMessage("That email is taken");
    }

    private void RulesForRoleName()
    {
        RuleFor(r => r.RoleName)
            .NotNull()
            .WithMessage("RoleName must not be null")
            .NotEmpty()
            .WithMessage("RoleName must not be empty")
            .Must(value => _allowedRoleName.Contains(value))
            .WithMessage($"RoleName must be in [{string.Join(SEPARATOR, _allowedRoleName)}]");
    }

    private void RulesForBirthDate()
    {
        RuleFor(r => r.Birthdate)
            .NotNull()
            .WithMessage("Birthdate must not be null")
            .NotEmpty()
            .WithMessage("Birthdate must not be empty")
            .Must(d => d?.AddYears(18) <= DateTime.UtcNow)
            .WithMessage("You must be at least 18 years old to create account");
    }

    private void RulesForPassword()
    {
        RuleFor(r => r.Password)
            .NotNull()
            .WithMessage("Password must not be null")
            .NotEmpty()
            .WithMessage("PasswordMustNotBeEmpty")
            .MinimumLength(10)
            .WithMessage("Password must contain minimum 10 characters");
    }

    private void RulesForConfirmPassword()
    {
        RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password)
            .WithMessage("The value of confirm password must be equal to the value of Password");
    }
}
