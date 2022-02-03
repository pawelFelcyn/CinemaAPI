using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal static class ModelBuilderExtensions
{
    public static ModelBuilder OnUserCreating(this ModelBuilder builder)
    {
        builder.Entity<User>()
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<User>()
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.RoleName)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.Birthdate)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.DateOfAppending)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        return builder;
    }

    public static ModelBuilder OnCinemaCreating(this ModelBuilder builder)
    {
        builder.Entity<Cinema>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Cinema>()
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Entity<Cinema>()
            .Property(c => c.CreatedById)
            .IsRequired();

        builder.Entity<Cinema>()
            .Property(c => c.AddressId)
            .IsRequired();

        builder.Entity<Cinema>()
            .Property(c => c.ContactEmail)
            .IsRequired();

        return builder;
    }

    public static ModelBuilder OnAddressCreating(this ModelBuilder builder)
    {
        builder.Entity<Address>()
            .Property(a => a.City)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Address>()
            .Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Address>()
            .Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(6);

        return builder;
    }

    public static ModelBuilder OnMovieCreating(this ModelBuilder builder)
    {
        builder.Entity<Movie>()
            .Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Movie>()
            .Property(m => m.Director)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<Movie>()
            .Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Entity<Movie>()
            .Property(m => m.CreatedById)
            .IsRequired();

        builder.Entity<Movie>()
            .Property(m => m.DateOfRelease)
            .IsRequired();

        builder.Entity<Movie>()
            .Property(m => m.Duration)
            .IsRequired();

        return builder;
    }

    public static ModelBuilder OnShowingCreating(this ModelBuilder builder)
    {
        builder.Entity<Showing>()
            .Property(s => s.Starts)
            .IsRequired();

        builder.Entity<Showing>()
            .Property(s => s.TicketsAmount)
            .IsRequired();

        builder.Entity<Showing>()
            .Property(s => s.CinemaId)
            .IsRequired();

        builder.Entity<Showing>()
            .Property(s => s.MovieId)
            .IsRequired();

        builder.Entity<Showing>()
            .Property(s => s.CreatedById)
            .IsRequired();

        return builder;
    }

    public static ModelBuilder OnReservationCreating(this ModelBuilder builder)
    {
        builder.Entity<Reservation>()
            .Property(r => r.ShowingId)
            .IsRequired();

        builder.Entity<Reservation>()
            .Property(r => r.HalfPriceTicketsAmount)
            .IsRequired();

        builder.Entity<Reservation>()
            .Property(r => r.FullPriceTicketsAmount)
            .IsRequired();

        builder.Entity<Reservation>()
            .Property(r => r.ReservedById)
            .IsRequired();

        return builder;
    }
}
