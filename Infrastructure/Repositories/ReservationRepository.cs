using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly CinemaContext _dbContext;
    private readonly IPageCreator<Reservation> _pageCreator;

    public ReservationRepository(CinemaContext dbContext, IPageCreator<Reservation> pageCreator)
    {
        _dbContext = dbContext;
        _pageCreator = pageCreator;
    }

    public Page<Reservation> GetAll(int cinemaId, int showingId, ResourceQuery query)
    {
        return _pageCreator?.CreatePage(_dbContext?
            .Reservations?
            .Include(r => r.ReservedBy)
            .Include(r => r.Showing.Movie)
            .Where(r => r.ShowingId == showingId && r.Showing.CinemaId == cinemaId), query);
    }

    public Reservation GetById(int cinemaId, int showingId, int reservationId)
    {
        throw new NotImplementedException();
    }

    public Reservation Add(Reservation reservation)
    {

        _dbContext?.Reservations?.Add(reservation);
        _dbContext?.SaveChanges();

        reservation =  _dbContext?
                       .Reservations?
                       .Include(r => r.Showing.Movie)
                       .Include(r => r.ReservedBy)
                       .FirstOrDefault(r => r == reservation);

        reservation.Showing.TicketsAmount -= reservation.FullPriceTicketsAmount + reservation.HalfPriceTicketsAmount;
        _dbContext?.SaveChanges();

        return reservation;
    }
}
