using Application.Dtos;
using Application.Services;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controllers;

public class CinemasControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    Mock<ICinemaService> _cinemaServiceMock = new();

    public CinemasControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var cinemaService = services.SingleOrDefault(s => s.ServiceType == typeof(ICinemaService));
                services.Remove(cinemaService);
                services.AddSingleton(_cinemaServiceMock.Object);
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetById_ForExistingCinema_ReturnsOkStatusCode()
    {
        _cinemaServiceMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(new CinemaDetailsDto());

        var response = await _client.GetAsync("api/Cinemas/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_ForonexistingCinema_ReturnsNotFoundStatusCode()
    {
        _cinemaServiceMock.Setup(m => m.GetById(It.IsAny<int>())).Throws(new CinemaNotFoundException());

        var response = await _client.GetAsync("api/Cinemas/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
