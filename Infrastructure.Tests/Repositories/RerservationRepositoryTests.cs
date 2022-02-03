using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class RerservationRepositoryTests
{
    private readonly ReservationRepository _repository;
    private readonly CinemaContext _dbContext;

    public RerservationRepositoryTests()
    {
        var pageCreatorMock = new Mock<IPageCreator<Reservation>>();
        var builder = new DbContextOptionsBuilder();
        builder.UseInMemoryDatabase("CinemaDb");
        _dbContext = new CinemaContext(builder.Options);
        _repository = new ReservationRepository(_dbContext, pageCreatorMock.Object);
    }

    [Fact]
    public void Add_ForGivenReservation_AddsItToDatabaseAndReturns()
    {
        var reservation = GetReservation();

        var result = _repository.Add(reservation);

        result.Should().Be(reservation);
        _dbContext.Reservations.Should().Contain(reservation);
    }

    private Reservation GetReservation() => new()
    {
        HalfPriceTicketsAmount = 1,
        FullPriceTicketsAmount = 1,
        ReservedBy = new()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "good@email.com",
            RoleName = "Manager",
            Birthdate = new System.DateTime(2000, 1, 1),
            PasswordHash = "SomePasswordHash"
        },
        Showing = new()
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
        }
    };
}
