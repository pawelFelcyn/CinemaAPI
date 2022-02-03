using Application.Dtos;
using Domain.Models;

namespace Application.Services;

public interface ICinemaService
{
    Page<CinemaDto> GetAll(ResourceQuery query);
    CinemaDetailsDto GetById(int id);
    CinemaDetailsDto Create(CreateCinemaDto dto);
    CinemaDetailsDto Update(UpdateCinemaDto dto, int id);
    void Delete(int id);
}
