using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Credentials;

public sealed class Credential : AggregateRoot<CredentialId>
{
    private Credential()
        : base(null!)
    {
    }

    private Credential(
        CredentialId id,
        Guid userId,
        PasswordHash passwordHash)
        : base(id)
    {
        UserId = userId;
        PasswordHash = passwordHash;

        CreatedOnUtc = DateTime.UtcNow;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public PasswordHash PasswordHash { get; private set; } = null!;

    public int FailedAccessCount { get; private set; }

    public DateTime? LockoutEndUtc { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime UpdatedOnUtc { get; private set; }

    public static Credential Create(
        Guid userId,
        PasswordHash passwordHash)
    {
        return new Credential(
            CredentialId.CreateUnique(),
            userId,
            passwordHash);
    }

    public void ChangePassword(
        PasswordHash passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void RegisterFailedAttempt()
    {
        FailedAccessCount++;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void ResetFailedAttempts()
    {
        FailedAccessCount = 0;
        LockoutEndUtc = null;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void LockUntil(DateTime utcDate)
    {
        LockoutEndUtc = utcDate;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public bool IsLocked()
    {
        return LockoutEndUtc.HasValue &&
               LockoutEndUtc.Value > DateTime.UtcNow;
    }
}
