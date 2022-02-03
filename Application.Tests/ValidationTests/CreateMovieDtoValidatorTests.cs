using Application.Dtos;
using Application.Tests.Helpers;
using Application.Tests.Services;
using Application.Validation;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.ValidationTests;

public class CreateMovieDtoValidatorTests : IClassFixture<CreateMovieDtoValidator>
{
    private readonly CreateMovieDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public CreateMovieDtoValidatorTests(CreateMovieDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    [Theory]
    [ClassData(typeof(InvalidCreateMovieDto))]
    [ClassData(typeof(ValidCreateMovieDto))]
    public void Validate_ForGivenModel_ReturnProperValidationResult(CreateMovieDto model, bool expected)
    {
        var validationResult = _validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().Be(expected);
    }
}
