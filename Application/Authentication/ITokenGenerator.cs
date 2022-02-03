using Domain.Entities;

namespace Application.Authentication;

public interface ITokenGenerator
{
    string GetTokenString(User user);
}
