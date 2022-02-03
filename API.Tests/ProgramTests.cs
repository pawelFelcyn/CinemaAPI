using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;

namespace API.Tests;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly List<Type> _controllerTypes;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _controllerTypes = typeof(Program)
            .Assembly
            .GetTypes()
            .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
            .ToList();

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                _controllerTypes.ForEach(c => services.AddScoped(c));
            });
        });
    }

    [Fact]
    public void DependencyInjection_ForControllers_RegistersAllDependencies()
    {
        var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory?.CreateScope();

        foreach (var type in _controllerTypes)
        {
            var controller = scope?.ServiceProvider.GetServices(type);
            controller.Should().NotBeNull();
        }
    }
}
