using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class ShowingRepositoryTests
{
    private readonly ShowingRepository _repository;
    private readonly CinemaContext _dbContext;

    public ShowingRepositoryTests()
    {
        var pageCreatorMock = new Mock<IPageCreator<Showing>>();
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("CinemaDb");
        _dbContext = new CinemaContext(builder.Options);
        _repository = new ShowingRepository(_dbContext, pageCreatorMock.Object);
    }

    [Fact]
    public void GetById_ForBadId_ThrowsShowingNotFoundException()
    {
        var action = () => _repository.GetById(0, 0);

        Assert.Throws<ShowingNotFoundException>(action);
    }

    [Fact]
    public void GetById_ForGoodId_ReturnsProperShowing()
    {
        var showing = SeedAndGetShowing();

        var result = _repository.GetById(showing.CinemaId, showing.Id);

        result.Should().Be(showing);
    }

    [Fact]
    public void Add_ForGivenShowing_AddsItToDatabaseAndReturns()
    {
        var showing = GetShowing();

        var result = _repository.Add(showing);

        result.Should().Be(showing);
        _dbContext?.Showings?.Should().Contain(showing);
    }

    [Fact]
    public void Remove_ForGivenShowing_RemovesItFromDatabase()
    {
        var showing = SeedAndGetShowing();

        _repository.Remove(showing);

        _dbContext.Showings.Should().NotContain(showing);
    }

    private Showing SeedAndGetShowing()
    {
        var showing = GetShowing();

        _dbContext?.Showings?.Add(showing);
        _dbContext?.SaveChanges();

        return showing;
    }

    private Showing GetShowing() => new()
    {
        Starts = System.DateTime.Now,
        TicketsAmount = 100,
        Cinema = new()
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
        },
        Movie = new()
        {
            Title = "Title",
            Director = "Director",
            Description = "Description",
            DateOfRelease = System.DateTime.Now,
            Duration = new System.TimeSpan(1, 0, 0),
            CreatedById = 1
        },
        CreatedById = 1
    };
}
