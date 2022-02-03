using Application.Dtos;
using Application.Validation;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Application.Tests.Helpers;

namespace Application.Tests.ValidationTests;

public class CreateReservationDtoValidatorTests : IClassFixture<CreateReservationDtoValidator>
{
    private readonly CreateReservationDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public CreateReservationDtoValidatorTests(CreateReservationDtoValidator  validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new CreateReservationDto(1, 1), true };
        yield return new object[] { new CreateReservationDto(2, 0), true };
        yield return new object[] { new CreateReservationDto(0, 5), true };
        yield return new object[] { new CreateReservationDto(-1, 10), false };
        yield return new object[] { new CreateReservationDto(5, -1), false };
        yield return new object[] { new CreateReservationDto(-2, -4), false };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationReesult(CreateReservationDto model, bool expected)
    {
        var validatioResult = _validator.Validate(model);

        _outputHelper.DisplayErrors(validatioResult.Errors);

        validatioResult.IsValid.Should().Be(expected);
    }
}
