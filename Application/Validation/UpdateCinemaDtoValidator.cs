using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class UpdateCinemaDtoValidator : AbstractValidator<UpdateCinemaDto>
{
    public UpdateCinemaDtoValidator()
    {
        RulesForName();
        RulesForDescription();
        RulesForContactEmail();
        RulesForPhoneNumber();
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
}
