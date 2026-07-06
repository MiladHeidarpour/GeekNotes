using GeekNotes.Modules.Idp.Domain;
using System.Security.Cryptography;

namespace GeekNotes.Modules.Idp.Infrastructure;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}