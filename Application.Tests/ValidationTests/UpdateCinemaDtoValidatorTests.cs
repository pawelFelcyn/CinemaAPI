using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Application.Dtos;
using Application.Validation;
using Application.Tests.Helpers;
using System.Collections.Generic;

namespace Application.Tests.ValidationTests;

public class UpdateCinemaDtoValidatorTests : IClassFixture<UpdateCinemaDtoValidator>
{
    private readonly UpdateCinemaDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public UpdateCinemaDtoValidatorTests(UpdateCinemaDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "email@email.com", "777888999"), true };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "email@email.com", ""), true };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "email@email.com", null), true };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "email@email.com", "777888"), false };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "invalid", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", "", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("Name", "Description", null, "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("Name", "", "email@email.com", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("Name", null, "email@email.com", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("", "Description", "email@email.com", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto(null, "Description", "email@email.com", "777888999"), false };
        yield return new object[] { new UpdateCinemaDto("very looooooooooooooooooooooooooooooooooooooooooooong", "Description", "email@email.com", "777888999"), false };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(UpdateCinemaDto model, bool expected)
    {
        var validationResult = _validator.Validate(model);
        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().Be(expected);
    }
}
