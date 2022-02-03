using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Domain.Modification;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class CinemaRepositoryTests
{
    private readonly CinemaRepository _repository;
    private readonly CinemaContext _dbContext;

    public CinemaRepositoryTests()
    {
        var pageCreatorMock = new Mock<IPageCreator<Cinema>>();
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("CinemaDb");
        _dbContext = new CinemaContext(builder.Options);
        _repository = new CinemaRepository(_dbContext, pageCreatorMock.Object);
    }

    [Fact]
    public void GetById_ForBadId_ThrowsCinemaNotFoundException()
    {
        var action = () => _repository.GetById(0);

        Assert.Throws<CinemaNotFoundException>(action);
    }

    [Fact]
    public void GetById_ForGoodId_ReturnsProperCinema()
    {
        var cinema = GetCinema();

        _dbContext?.Cinemas?.Add(cinema);
        _dbContext?.SaveChanges();

        var result = _repository.GetById(cinema.Id);

        result.Should().Be(result);
    }

    [Fact]
    public void Add_ForGivenCinema_AddsItToDatabaseAndReturns()
    {
        var cinema = GetCinema();

        var result = _repository.Add(cinema);

        result.Should().Be(cinema);
        _dbContext.Cinemas.Should().Contain(cinema);
    }

    [Fact]
    public void Update_ForGivenArguments_UpdatesProperCinema()
    {
        var cinema = GetCinema();
        var modParams = new CinemaModificationParams()
        {
            Name = "Updated",
            Description = "Updated",
            ContactEmail = "Updated",
            PhoneNumber = "Updated"
        };

        var result = _repository.Update(cinema, modParams);

        result.Should().Be(cinema);
        cinema.Name.Should().Be(modParams.Name);
        cinema.Description.Should().Be(modParams.Description);
        cinema.ContactEmail.Should().Be(modParams.ContactEmail);
        cinema.PhoneNumber.Should().Be(modParams.PhoneNumber);
    }

    [Fact]
    public void Remove_ForGivenCinema_RemovesItFromDatabase()
    {
        var cinema = GetCinema();

        _dbContext?.Cinemas?.Add(cinema);
        _dbContext?.SaveChanges();

        _repository.Remove(cinema);

        _dbContext?.Cinemas.Should().NotContain(cinema);
    }

    private Cinema GetCinema() => new Cinema()
    {
        Name = "Name",
        Description = "Description",
        ContactEmail = "contact@email.com",
        PhoneNumber = "987654321",
        CreatedById = 1,
        Address = new Address()
        {
            City = "City",
            Street = "Street",
            PostalCode = "00-000"
        }
    };
}
