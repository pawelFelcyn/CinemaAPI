using Application.Dtos;
using Application.Tests.Helpers;
using Application.Validation;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.ValidationTests;

public class CreateShowingDtoValidatiorTests
{
    private readonly ITestOutputHelper _outputHelper;

    public CreateShowingDtoValidatiorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new CreateShowingDto(new DateTime(2030, 1, 1), 100, 1), true, true };
        yield return new object[] { new CreateShowingDto(new DateTime(2030, 1, 1), 1, 1), true, true };
        yield return new object[] { new CreateShowingDto(new DateTime(2030, 1, 1), 100, 1), false, false };
        yield return new object[] { new CreateShowingDto(DateTime.UtcNow, 100, 1), true, false };
        yield return new object[] { new CreateShowingDto(new DateTime(2015, 1, 1), 100, 1), true, false };
        yield return new object[] { new CreateShowingDto(new DateTime(2030, 1, 1), 0, 1), true, false };
        yield return new object[] { new CreateShowingDto(new DateTime(2030, 1, 1), -5, 1), true, false };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(CreateShowingDto model, bool movieExists, bool expectedResult)
    {
        var validationHelperMock = new Mock<IMovieIdValidationHelper>();
        validationHelperMock.Setup(m => m.Exists(It.IsAny<int>())).Returns(movieExists);
        var validator = new CreateShowingDtoValidator(validationHelperMock.Object);


        var validationResult = validator.Validate(model);
        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().Be(expectedResult);
    }
}
