using Application.Dtos;
using Application.Validation;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using System.Collections.Generic;
using Application.Tests.Helpers;

namespace Application.Tests.ValidationTests;

public class LoginDtoValidatorTests : IClassFixture<LoginDtoValidator>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly LoginDtoValidator _validator;

    public LoginDtoValidatorTests(ITestOutputHelper outputHelper, LoginDtoValidator validator)
    {
        _outputHelper = outputHelper;
        _validator = validator;
    }

    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new LoginDto("valid@email.com", "Password"), true };
        yield return new object[] { new LoginDto("valid@email.com", null), false };
        yield return new object[] { new LoginDto("valid@email.com", ""), false };
        yield return new object[] { new LoginDto("invalid_email", "Password"), false };
        yield return new object[] { new LoginDto("", "Password"), false };
        yield return new object[] { new LoginDto(null, "Password"), false };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Validate_ForGivenModel_ReturnsProperValidationResult(LoginDto model, bool expected)
    {
        var validationResult = _validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().Be(expected);
    }
}
