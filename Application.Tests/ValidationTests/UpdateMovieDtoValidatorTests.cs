using Application.Dtos;
using Application.Tests.Helpers;
using Application.Validation;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.ValidationTests;

public class UpdateMovieDtoValidatorTests : IClassFixture<UpdateMovieDtoValidator>
{
    private readonly UpdateMovieDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public UpdateMovieDtoValidatorTests(UpdateMovieDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new UpdateMovieDto("Description"), true };
        yield return new object[] { new UpdateMovieDto(""), false };
        yield return new object[] { new UpdateMovieDto(null), false };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(UpdateMovieDto model, bool expected)
    {
        var validationResult = _validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().Be(expected);
    }
}
