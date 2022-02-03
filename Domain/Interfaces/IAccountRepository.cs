using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    void Add(User user);
    User GetByEmail(string email);
}
