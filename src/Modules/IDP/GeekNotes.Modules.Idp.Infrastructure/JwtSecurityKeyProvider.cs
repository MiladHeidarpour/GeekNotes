using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace GeekNotes.Modules.Idp.Infrastructure;

public sealed class JwtSecurityKeyProvider
{
    private readonly RSA _rsa;

    public JwtSecurityKeyProvider(IHostEnvironment env)
    {
        _rsa = RSA.Create();

        var path = Path.Combine(env.ContentRootPath, "crypto_key");

        if (File.Exists(path))
        {
            var keyBytes = File.ReadAllBytes(path);
            _rsa.ImportRSAPrivateKey(keyBytes, out _);
        }
        else
        {
            var keyBytes = _rsa.ExportRSAPrivateKey();
            File.WriteAllBytes(path, keyBytes);
        }
    }

    public RsaSecurityKey GetKey()
        => new(_rsa);

    public SigningCredentials GetSigningCredentials()
        => new(GetKey(), SecurityAlgorithms.RsaSha256);
}
