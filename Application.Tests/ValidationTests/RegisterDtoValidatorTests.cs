using Application.Dtos;
using Application.Tests.Helpers;
using Application.Tests.Services;
using Application.Validation;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.ValidationTests;

public class RegisterDtoValidatorTests
{
    private readonly ITestOutputHelper _outputHelper;

    public RegisterDtoValidatorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Validate_ForValidModel_ValidationSucceed()
    {
        var model = new RegisterDto("Firstname", "Lastname", "email@email.com", "Manager",
            new System.DateTime(1990, 1, 1), "Password123", "Password123");
        var validationHelperMock = new Mock<IEmailValidationHelper>();
        validationHelperMock.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(false);
        var validator = new RegisterDtoValidator(validationHelperMock.Object);

        var validationResult = validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validete_ForExistingEmail_ValidationNotSucceed()
    {
        var model = GetModel();
        var validationHelperMock = new Mock<IEmailValidationHelper>();
        validationHelperMock.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(true);
        var validator = new RegisterDtoValidator(validationHelperMock.Object);

        var validationResult = validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().BeFalse();
    }

    [Theory]
    [ClassData(typeof(InvalidRegister))]
    public void Validate_ForInvalidModel_ValidationNotSucceed(RegisterDto model)
    {
        var validationHelperMock = new Mock<IEmailValidationHelper>();
        validationHelperMock.Setup(m => m.IsTaken(It.IsAny<string>())).Returns(false);
        var validator = new RegisterDtoValidator(validationHelperMock.Object);

        var validationResult = validator.Validate(model);

        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().BeFalse();
    }

    private RegisterDto GetModel() => new RegisterDto("Firstname", "Lastname", "email@email.com", "Manager",
            new System.DateTime(1990, 1, 1), "Password123", "Password123");
}
