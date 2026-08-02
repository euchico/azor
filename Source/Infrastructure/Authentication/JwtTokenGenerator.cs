using Application.Authentication;
using Infrastructure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public interface IJwtTokenGenerator
{
    AuthResponse Create(ApplicationUser user);
}

public sealed class JwtTokenGenerator(IOptions<JwtSettings> options) : IJwtTokenGenerator
{
    private readonly JwtSettings _settings = options.Value;

    public AuthResponse Create(ApplicationUser user)
    {
        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_settings.ExpirationMinutes);
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            ],
            expires: expiresAt.UtcDateTime,
            signingCredentials: signingCredentials);

        return new AuthResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            expiresAt,
            new AuthenticatedUser(user.Id, user.Email!));
    }
}
