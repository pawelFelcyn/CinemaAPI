using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class CreateCinemaDtoValidator : AbstractValidator<CreateCinemaDto>
{
    public CreateCinemaDtoValidator()
    {
        RulesForName();
        RulesForDescription();
        RulesForContactEmail();
        RulesForPhoneNumber();
        RulesForCity();
        RulesForStreet();
        RulesForPostalCode();
    }

    private void RulesForName()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .WithMessage("Name must not be null")
            .NotEmpty()
            .WithMessage("Name must not be empty")
            .MaximumLength(30)
            .WithMessage("Maximum length of Name is 30 characters");
    }

    private void RulesForDescription()
    {
        RuleFor(c => c.Description)
            .NotNull()
            .WithMessage("Description must not be null")
            .NotEmpty()
            .WithMessage("Description must not be empty")
            .MaximumLength(2000)
            .WithMessage("Maximum length of Description is 2000 characters");
    }

    private void RulesForContactEmail()
    {
        RuleFor(c => c.ContactEmail)
            .NotNull()
            .WithMessage("ContactEmail must not be null")
            .NotEmpty()
            .WithMessage("ContactEmail must not be empty")
            .EmailAddress()
            .WithMessage("Tis is not a valid email address");
    }

    private void RulesForPhoneNumber()
    {
        RuleFor(c => c.PhoneNumber)
            .Custom((value, context) =>
            {
                if (value != null && value != string.Empty && !value.IsPhoneNumber())
                {
                    context.AddFailure("PhoneNumber", "This is not a valid phone number");
                }
            });
    }

    private void RulesForCity()
    {
        RuleFor(c => c.City)
            .NotNull()
            .WithMessage("City must not be null")
            .NotEmpty()
            .WithMessage("City must not be empty")
            .MaximumLength(30)
            .WithMessage("The maximum length of City is 30 characters");
    }

    private void RulesForStreet()
    {
        RuleFor(c => c.Street)
            .NotNull()
            .WithMessage("Street must not be null")
            .NotEmpty()
            .WithMessage("Street must not be empty")
            .MaximumLength(50)
            .WithMessage("The maximum length of Street is 50 characters");
    }

    private void RulesForPostalCode()
    {
        RuleFor(c => c.PostalCode)
            .NotNull()
            .WithMessage("PostalCode must not be null")
            .NotEmpty()
            .WithMessage("PostalCode must not be empty")
            .Custom((value, context) =>
            {
                if (value != null && !value.IsPostalCode())
                {
                    context.AddFailure("PostalCode", "This is not a valid postal code");
                }
            });
    }
}
