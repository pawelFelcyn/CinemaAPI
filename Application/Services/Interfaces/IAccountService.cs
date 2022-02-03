using Application.Dtos;

namespace Application.Services;

public interface IAccountService
{
    void Register(RegisterDto dto);
    string GetJwtToken(LoginDto dto);
}
