using Domain.Entities;
using Domain.Exceptions;
using Domain.Modification;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class MovieRepositoryTests
{
    private readonly MovieRepository _repository;
    private readonly CinemaContext _dbContext;

    public MovieRepositoryTests()
    {
        var pageCreatorMock = new Mock<IPageCreator<Movie>>();
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("CinemaDb");
        _dbContext = new CinemaContext(builder.Options);
        _repository = new MovieRepository(_dbContext, pageCreatorMock.Object);
    }

    [Fact]
    public void GetById_ForBadId_ThrowsMovieNotFoundException()
    {
        var action = () => _repository.GetById(0);

        Assert.Throws<MovieNotFoundException>(action);
    }

    [Fact]
    public void GetById_ForGoodId_ReturnsProperMovie()
    {
        var movie = GetMovie();

        _dbContext?.Movies?.Add(movie);
        _dbContext?.SaveChanges();

        var result = _repository.GetById(movie.Id);

        result.Should().Be(movie);
    }

    [Fact]
    public void Add_ForGivenMovie_AddsItToDatabaseAndReturns()
    {
        var movie = GetMovie();

        var result = _repository.Add(movie);

        result.Should().Be(movie);
        _dbContext.Movies.Should().Contain(movie);
    }

    [Fact]
    public void Update_ForGivenArguments_UpdatesProperMovie()
    {
        var movie = GetMovie();
        var modParams = new MovieModificationParams() { Description = "Description" };

        var result = _repository.Update(movie, modParams);

        result.Should().Be(movie);
        movie.Description.Should().Be(modParams.Description);
    }

    [Fact]
    public void Remove_ForGivenMovie_RemovesItFromDatabase()
    {
        var movie = GetMovie();

        _dbContext?.Movies?.Add(movie);
        _dbContext?.SaveChanges();
        
        _repository.Remove(movie);

        _dbContext?.Movies.Should().NotContain(movie);
    }

    private Movie GetMovie() => new()
    {
        Title = "Title",
        Director = "Director",
        Description = "Description",
        DateOfRelease = System.DateTime.Now,
        Duration = new System.TimeSpan(1, 0, 0),
        CreatedById = 1
    };
}
