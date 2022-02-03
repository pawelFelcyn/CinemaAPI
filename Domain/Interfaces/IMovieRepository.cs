using Domain.Entities;
using Domain.Models;
using Domain.Modification;

namespace Domain.Interfaces;

public interface IMovieRepository
{
    Page<Movie> GetAll(ResourceQuery query);
    Movie GetById(int id);
    Movie Add(Movie movie);
    Movie Update(Movie movie, MovieModificationParams modParams);
    void Remove(Movie movie);
}
