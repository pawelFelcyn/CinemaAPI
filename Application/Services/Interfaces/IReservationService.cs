using Application.Dtos;
using Domain.Models;

namespace Application.Services;

public interface IReservationService
{
    Page<ReservationDto> GetAll(int cinemaId, int showingId, ResourceQuery query);
    ReservationDto GetById(int cinemaId, int showingId, int resrevationId);
    ReservationDto Create(int cinemaId, int showingId, CreateReservationDto dto);
}
