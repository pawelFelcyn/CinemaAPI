using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Helper;

public class EmailValidationHelper : IEmailValidationHelper
{
    private readonly CinemaContext _dbContext;

    public EmailValidationHelper(CinemaContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsTaken(string email)
        => _dbContext.Users.Any(u => u.Email == email);
}
