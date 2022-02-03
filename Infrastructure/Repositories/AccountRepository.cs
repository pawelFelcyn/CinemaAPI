using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly CinemaContext _dbContext;

    public AccountRepository(CinemaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext?.Users?.Add(user);
        _dbContext?.SaveChanges();
    }

    public User GetByEmail(string email)
    {
        var user = _dbContext?.Users?
                   .FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            throw new InvalidEmailException();
        }

        return user;
    }
}
