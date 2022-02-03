using Application.Dtos;
using FluentValidation;

namespace Application.Validation;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RulesForFullPriceTicketsAmount();
        RulesForHalfPriceTicketsAmount();
    }

    private void RulesForFullPriceTicketsAmount()
    {
        RuleFor(c => c.FullPriceTicketsAmount)
            .NotNull()
            .WithMessage("FullProceTicketsAmount must not be null")
            .GreaterThanOrEqualTo(0)
            .WithMessage("FullPriceTicketsAmount must be greather than or equal to 0");
    }

    private void RulesForHalfPriceTicketsAmount()
    {
        RuleFor(c => c.HalfPriceTicketsAmount)
            .NotNull()
            .WithMessage("HalfProceTicketsAmount must not be null")
            .GreaterThanOrEqualTo(0)
            .WithMessage("HalfPriceTicketsAmount must be greather than or equal to 0");
    }
}
