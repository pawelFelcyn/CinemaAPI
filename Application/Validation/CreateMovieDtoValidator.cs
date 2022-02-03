using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
{
    public CreateMovieDtoValidator()
    {
        RulesForTitle();
        RulesForDirector();
        RulesForDescription();
        RulesForDateOfRelease();
        RulesForDuration();
    }

    private void RulesForTitle()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .WithMessage("Title must not be null")
            .NotEmpty()
            .WithMessage("Title must not be empty")
            .MaximumLength(100)
            .WithMessage("The maximum length of Title is 100 characters");
    }

    private void RulesForDirector()
    {
        RuleFor(c => c.Director)
            .NotNull()
            .WithMessage("Director must not be null")
            .NotEmpty()
            .WithMessage("Director must not be empty")
            .MaximumLength(30)
            .WithMessage("The maximum length of Director is 30 characters");
    }

    private void RulesForDescription()
    {
        RuleFor(c => c.Description)
            .NotNull()
            .WithMessage("Description must not be null")
            .NotEmpty()
            .WithMessage("Description must not be empty")
            .MaximumLength(2000)
            .WithMessage("The maximum length of Description is 2000 characters");
    }

    private void RulesForDateOfRelease()
    {
        RuleFor(c => c.DateOfRelease)
            .NotNull()
            .WithMessage("DateOfRelease must not be null")
            .NotEmpty()
            .WithMessage("DateOfRelease must not be empty")
            .Must(d => d is null || d < DateTime.UtcNow)
            .WithMessage("DateOfRelease must be earlier than now");
    }

    private void RulesForDuration()
    {
        RuleFor(c => c.FilmDuration)
            .NotNull()
            .WithMessage("FilmDuration must not be null")
            .NotEmpty()
            .WithMessage("FilmDuration must not be empty")
            .Must(d => d is null || TimeSpan.TryParse(d, out var span))
            .WithMessage("FilmDuration can not be converted to TimeSpan")
            .Custom((value, context) =>
            {
                if (TimeSpan.TryParse(value, out var span))
                {
                    if (span > new TimeSpan(5, 0, 0))
                        context.AddFailure("FilmDuration", "FilmDuration must not be greather than 5 hours");
                    else if (span < new TimeSpan(0, 30, 0))
                        context.AddFailure("FilmDuration", "FilmDuration must not be less than 30 minutes");
                }
            });
    }
}
