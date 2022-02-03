using Domain.Models;
using FluentValidation;

namespace Application.Validation;

public class ResourceQueryValidator<T> : AbstractValidator<T> where T : ResourceQuery
{
    private readonly string[] _alloewdSortByColumnNames;
    private readonly int[] _allowedPageSizes = new[] { 5, 10, 15 };
    private const char SEPARATOR = ',';

    public ResourceQueryValidator()
    {
        _alloewdSortByColumnNames = SortByColumnNames.SortByTypesSelector[typeof(T)];

        RulesForPageSize();
        RulesForPageNumber();
        RulesForSortBy();
        RulesForSortDirection();
    }

    private void RulesForPageSize()
    {
        RuleFor(q => q.PageSize)
            .Must(ps => _allowedPageSizes.Contains(ps))
            .WithMessage($"PageSize must be in [{string.Join(SEPARATOR, _allowedPageSizes)}]");
    }

    private void RulesForPageNumber()
    {
        RuleFor(q => q.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greather than 0");
    }

    private void RulesForSortBy()
    {
        RuleFor(q => q.SortBy)
            .Must(sb => _alloewdSortByColumnNames.Contains(sb))
            .WithMessage($"SortBy must be in [{string.Join(SEPARATOR, _alloewdSortByColumnNames)}]");
    }

    private void RulesForSortDirection()
    {
        RuleFor(q => q.SortDirection)
            .IsInEnum()
            .WithMessage("Sort direction must be in [asc,desc]");
    }
}
