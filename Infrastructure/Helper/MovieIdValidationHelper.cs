using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Helper;

public class MovieIdValidationHelper : IMovieIdValidationHelper
{
    private readonly CinemaContext _dbContext;

    public MovieIdValidationHelper(CinemaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(int id)
        => _dbContext.Movies.Any(m => m.Id == id);
}
