using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CinemaContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Cinema>? Cinemas { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Movie>? Movies { get; set; }
    public DbSet<Showing>? Showings { get; set; }
    public DbSet<Reservation>? Reservations { get; set; }

    public CinemaContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.OnUserCreating()
                    .OnCinemaCreating()
                    .OnAddressCreating()
                    .OnMovieCreating()
                    .OnShowingCreating()
                    .OnReservationCreating();
    }
}
