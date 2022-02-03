using Application.Dtos;
using Application.Tests.Helpers;
using Application.Tests.Services;
using Application.Validation;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Application.Tests.ValidationTests;

public class CreateCinemaDtoValidatorTests : IClassFixture<CreateCinemaDtoValidator>
{
    private readonly CreateCinemaDtoValidator _validator;
    private readonly ITestOutputHelper _outputHelper;

    public CreateCinemaDtoValidatorTests(CreateCinemaDtoValidator validator, ITestOutputHelper outputHelper)
    {
        _validator = validator;
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Validate_ForValidModel_ValidationSucceed()
    {
        var model = new CreateCinemaDto("Name", "Description", "email@email.com", "999999999", "City", "Street", "00-000");

        var validationResult = _validator.Validate(model);
        _outputHelper.DisplayErrors(validationResult.Errors);

        validationResult.IsValid.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidCreateCinemaDto))]
    public void Validate_ForInvalidModel_ValidationNotSucceed(CreateCinemaDto model)
    {
        var validationResult = _validator.Validate(model);
        _outputHelper.DisplayErrors(validationResult.Errors);


        validationResult.IsValid.Should().BeFalse();
    }
}
