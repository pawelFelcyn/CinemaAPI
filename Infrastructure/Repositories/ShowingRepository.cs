using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShowingRepository : IShowingRepository
{
    private readonly CinemaContext _dbContext;
    private readonly IPageCreator<Showing> _pageCreator;

    public ShowingRepository(CinemaContext dbContext, IPageCreator<Showing> pageCreator)
    {
        _dbContext = dbContext;
        _pageCreator = pageCreator;
    }

    public Page<Showing> GetAll(int cinemaId, ResourceQuery query)
    {
        return _pageCreator.CreatePage(
            _dbContext?
            .Showings?
            .Include(s => s.Movie)
            .Where(s => s.Starts >= DateTime.UtcNow), query);
    }

    public Showing GetById(int cinemaId, int showingId)
    {
        var showing = _dbContext?
                      .Showings?
                      .Include(s => s.Movie)
                      .Include(s => s.Cinema)
                      .FirstOrDefault(s => s.CinemaId == cinemaId && s.Id == showingId);

        if (showing == null)
        {
            throw new ShowingNotFoundException();
        }

        return showing;
    }

    public Showing Add(Showing showing)
    {
        _dbContext?.Showings?.Add(showing);
        _dbContext?.SaveChanges();

        return _dbContext?
               .Showings?
               .Include(s => s.Cinema)
               .Include(s => s.Movie)
               .FirstOrDefault(s => s == showing);
    }

    public void Remove(Showing showing)
    {
        _dbContext?.Showings?.Remove(showing);
        _dbContext?.SaveChanges();
    }
}
