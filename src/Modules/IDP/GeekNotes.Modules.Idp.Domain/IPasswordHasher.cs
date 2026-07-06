using GeekNotes.Modules.Idp.Domain.Credentials;

namespace GeekNotes.Modules.Idp.Domain;

public interface IPasswordHasher
{
    PasswordHash Hash(string password);

    PasswordVerificationStatus Verify(
        PasswordHash passwordHash,
        string password);
}