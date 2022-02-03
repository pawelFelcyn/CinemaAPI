using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Modification;
using Infrastructure.Data;
using Infrastructure.Pagination;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaContext _dbContext;
    private readonly IPageCreator<Movie> _pageCreator;

    public MovieRepository(CinemaContext dbContext, IPageCreator<Movie> pageCreator)
    {
        _dbContext = dbContext;
        _pageCreator = pageCreator;
    }

    public Page<Movie> GetAll(ResourceQuery query)
    {
        return _pageCreator.CreatePage(_dbContext.Movies, query);
    }

    public Movie GetById(int id)
    {
        var movie = _dbContext?
                    .Movies?
                    .FirstOrDefault(m => m.Id == id);

        if (movie == null)
        {
            throw new MovieNotFoundException();
        }

        return movie;
    }

    public Movie Add(Movie movie)
    {
        _dbContext?.Movies?.Add(movie);
        _dbContext?.SaveChanges();

        return movie;
    }

    public Movie Update(Movie movie, MovieModificationParams modParams)
    {
        movie.Description = modParams.Description;
        _dbContext?.SaveChanges();

        return movie;
    }

    public void Remove(Movie movie)
    {
        _dbContext?.Movies?.Remove(movie);
        _dbContext?.SaveChanges();
    }
}
