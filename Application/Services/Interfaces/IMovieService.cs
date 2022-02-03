using Application.Dtos;
using Domain.Models;

namespace Application.Services;

public interface IMovieService
{
    Page<MovieDto> GetAll(ResourceQuery query);
    MovieDetailsDto GetById(int id);
    MovieDetailsDto Create(CreateMovieDto dto);
    MovieDetailsDto Update(UpdateMovieDto dto, int id);
    void Delete(int id);
}
