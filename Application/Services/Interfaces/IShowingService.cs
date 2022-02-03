using Application.Dtos;
using Domain.Models;

namespace Application.Services;

public interface IShowingService
{
    Page<ShowingDto> GetAll(int cinemaId, ResourceQuery query);
    ShowingDetailsDto GetById(int cinemaId, int showingId);
    ShowingDetailsDto Create(int cinemaId, CreateShowingDto dto);
    void Delete(int cinemaId, int showingId);
}
