using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authentication;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly AuthenticationSettings _authenticationSettings;

    public JwtTokenGenerator(AuthenticationSettings authenticationSettings)
    {
        _authenticationSettings = authenticationSettings;
    }

    public string GetTokenString(User user)
    {
        var claims = GetClaims(user);
        var token = GenerateToken(claims);

        var hanlder = new JwtSecurityTokenHandler();

        return hanlder.WriteToken(token);
    }

    private IEnumerable<Claim> GetClaims(User user)
        => new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleName),
        };

    private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);

        return new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: credentails);
    }
}
