using Application.Dtos;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validation;

public class CreateShowingDtoValidator : AbstractValidator<CreateShowingDto>
{
    private readonly IMovieIdValidationHelper _validationHelper;

    public CreateShowingDtoValidator(IMovieIdValidationHelper validationHelper)
    {
        _validationHelper = validationHelper;

        RulesForStarts();
        RulesForTicketsAmount();
        RulesForMovieId();
    }

    private void RulesForStarts()
    {
        RuleFor(c => c.Starts)
            .NotNull()
            .WithMessage("Starts must not be null")
            .NotEmpty()
            .WithMessage("Starts must not be empty")
            .Must(s => s > DateTime.UtcNow)
            .WithMessage("Starts must be later than now");
    }

    private void RulesForTicketsAmount()
    {
        RuleFor(c => c.TicketsAmount)
            .GreaterThan(0)
            .WithMessage("TicketsAmount must be grather than 0");
    }

    private void RulesForMovieId()
    {
        RuleFor(c => c.MovieId)
            .Must(m => _validationHelper.Exists(m))
            .WithMessage("Movie with given id does not exist");
    }
}
