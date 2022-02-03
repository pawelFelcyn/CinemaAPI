using Domain.Entities;
using Domain.Models;

namespace Domain.Interfaces;

public interface IReservationRepository
{
    Page<Reservation> GetAll(int cinemaId, int showingId, ResourceQuery query);
    Reservation GetById(int cinemaId, int showingId, int reservationId);
    Reservation Add(Reservation reservation);
}
