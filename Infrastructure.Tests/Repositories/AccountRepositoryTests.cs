using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain.Exceptions;
using Domain.Entities;
using FluentAssertions;

namespace Infrastructure.Tests.Repositories;

public class AccountRepositoryTests 
{
    private readonly AccountRepository _repository;
    private readonly CinemaContext _dbContext;

    public AccountRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("CinemaDb");
        _dbContext = new CinemaContext(builder.Options);
        _repository = new AccountRepository(_dbContext);
    }

    [Fact]
    public void GetByEmail_ForBadEmail_ThrowsInvalidEmailException()
    {
        var action = () => _repository.GetByEmail("nonExistingEmail");

        Assert.Throws<InvalidEmailException>(action);
    }

    [Fact]
    public void GetByEmail_ForGoodEmail_ReturnsProperUser()
    {
        var user = GetUser();

        _dbContext?.Users?.Add(user);
        _dbContext?.SaveChanges();

        var result = _repository.GetByEmail(user.Email);

        result.Should().Be(user);
    }

    [Fact]
    public void Add_ForGivenModel_AddsItToDatabase()
    {
        var user = GetUser();

        _repository.Add(user);

        _dbContext.Users.Should().Contain(user);
    }

    private User GetUser() => new User()
    {
        FirstName = "FirstName",
        LastName = "LastName",
        Email = "good@email.com",
        RoleName = "Manager",
        Birthdate = new System.DateTime(2000, 1, 1),
        PasswordHash = "SomePasswordHash"
    };
}
