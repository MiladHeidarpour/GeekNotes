using GeekNotes.Modules.Idp.Domain;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GeekNotes.Modules.Idp.Infrastructure;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSecurityKeyProvider _keyProvider;
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(
        JwtSecurityKeyProvider keyProvider,
        IOptions<JwtSettings> options)
    {
        _keyProvider = keyProvider;
        _settings = options.Value;
    }

    public string GenerateAccessToken(
        Guid userId,
        Guid sessionId,
        string email,
        IEnumerable<string> roles,
        IEnumerable<string> permissions)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Sid, sessionId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(x =>
            new Claim(ClaimTypes.Role, x)));

        claims.AddRange(permissions.Select(x =>
            new Claim("permission", x)));

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            signingCredentials: _keyProvider.GetSigningCredentials()
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}