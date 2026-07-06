using GeekNotes.Modules.Idp.Domain;
using GeekNotes.Modules.Idp.Domain.Credentials;
using Microsoft.AspNetCore.Identity;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Services;

internal sealed class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _passwordHasher =
        new();

    private static readonly object User = new();

    public PasswordHash Hash(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var hash = _passwordHasher.HashPassword(
            User,
            password);

        return PasswordHash.Create(hash);
    }

    public PasswordVerificationStatus Verify(
    PasswordHash passwordHash,
    string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            User,
            passwordHash.Value,
            password);

        return result switch
        {
            Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success
                => PasswordVerificationStatus.Success,

            Microsoft.AspNetCore.Identity.PasswordVerificationResult.SuccessRehashNeeded
                => PasswordVerificationStatus.SuccessRehashNeeded,

            _ => PasswordVerificationStatus.Failed
        };
    }
}