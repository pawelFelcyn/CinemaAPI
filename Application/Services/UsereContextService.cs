using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services;

public class UsereContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsereContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

    public int UserId => int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
