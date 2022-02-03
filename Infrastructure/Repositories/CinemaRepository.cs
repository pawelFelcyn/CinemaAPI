using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Modification;
using Infrastructure.Pagination;
using Domain.Models;

namespace Infrastructure.Repositories;

public class CinemaRepository : ICinemaRepository
{
    private readonly CinemaContext _dbContext;
    private readonly IPageCreator<Cinema> _pageCreator;

    public CinemaRepository(CinemaContext dbContext, IPageCreator<Cinema> pageCreator)
    {
        _dbContext = dbContext;
        _pageCreator = pageCreator;
    }

    public Page<Cinema> GetAll(ResourceQuery query)
    {
        return _pageCreator.CreatePage(_dbContext.Cinemas, query);
    }

    public Cinema GetById(int id)
    {
        var cinema = _dbContext?
                     .Cinemas?
                     .Include(c => c.Address)
                     .FirstOrDefault(c => c.Id == id);
        if (cinema == null)
        {
            throw new CinemaNotFoundException();
        }

        return cinema;
    }

    public Cinema Add(Cinema cinema)
    {
        _dbContext?.Cinemas?.Add(cinema);
        _dbContext?.SaveChanges();

        return cinema;
    }

    public Cinema Update(Cinema cinema, CinemaModificationParams updated)
    {
        cinema.Name = updated.Name;
        cinema.Description = updated.Description;
        cinema.ContactEmail = updated.ContactEmail;
        cinema.PhoneNumber = updated.PhoneNumber;
        _dbContext.SaveChanges();

        return cinema;
    }

    public void Remove(Cinema cinema)
    {
        _dbContext?.Cinemas?.Remove(cinema);
        _dbContext?.SaveChanges();
    }
}
