using Application.Dtos;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using FluentValidation.Results;
using System.Linq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using FluentAssertions;
using System.IO;
using API.Tests.Helpers;
using Domain.Exceptions;
using System.Collections;
using System.Collections.Generic;

namespace API.Tests.Controllers;

public class AccountsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    Mock<IAccountService> _accountServiceMock = new();
    private const string JWT_TOKEN = "jwtToken";

    public AccountsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var accountService = services.SingleOrDefault(s => s.ServiceType == typeof(IAccountService));
                services.Remove(accountService);
                services.AddSingleton(_accountServiceMock.Object);
            });
        });

        _client = _factory.CreateClient();
    }

    public static IEnumerable<object[]> GetExteptionTypes()
    {
        yield return new object[] { new InvalidPasswordException() };
        yield return new object[] { new InvalidEmailException() };
    }

    [Fact]
    public async Task Login_ForRegisteredUser_ReturntOkStatusCode()
    {
        var model = new LoginDto("email@email.com", "password");
        var content = model.GetJsonContent();
        _accountServiceMock.Setup(m => m.GetJwtToken(It.IsAny<LoginDto>())).Returns(JWT_TOKEN);

        var response = await _client.PostAsync("/api/Accounts/login", content);
        var contentStr =  await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        contentStr.Should().Be(JWT_TOKEN);
    }

    [Theory]
    [MemberData(nameof(GetExteptionTypes))]
    public async Task Login_ForNonregisteredUser_ReturnsBadRequestStatusCode<TException>(TException exception)
        where TException : BadRequestException
    {
        var model = new LoginDto("email@email.com", "password");
        var content = model.GetJsonContent();
        _accountServiceMock.Setup(m => m.GetJwtToken(It.IsAny<LoginDto>())).Throws(exception);

        var response = await _client.PostAsync("/api/Accounts/login", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
