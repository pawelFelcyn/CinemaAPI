using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
{
    public UpdateMovieDtoValidator()
    {
        RulesForDescription();
    }

    private void RulesForDescription()
    {
        RuleFor(u => u.Description)
            .NotNull()
            .WithMessage("Description must not be null")
            .NotEmpty()
            .WithMessage("Description must not be empty")
            .MaximumLength(2000)
            .WithMessage("The length of the Description must be lower or equal to 2000 characters");
    }
}
