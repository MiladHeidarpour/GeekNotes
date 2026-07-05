using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Verifications;

public sealed class Verification : AggregateRoot<VerificationId>
{
    private Verification()
        : base(null!)
    {
    }

    private Verification(
        VerificationId id,
        Guid userId,
        VerificationType type,
        VerificationCode code,
        DateTime expiresOnUtc)
        : base(id)
    {
        UserId = userId;
        Type = type;
        Code = code;
        ExpiresOnUtc = expiresOnUtc;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public VerificationType Type { get; private set; }

    public VerificationCode Code { get; private set; } = null!;

    public DateTime ExpiresOnUtc { get; private set; }

    public DateTime? UsedOnUtc { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Verification Create(
        Guid userId,
        VerificationType type,
        VerificationCode code,
        DateTime expiresOnUtc)
    {
        return new Verification(
            VerificationId.CreateUnique(),
            userId,
            type,
            code,
            expiresOnUtc);
    }

    public void MarkAsUsed()
    {
        if (UsedOnUtc is not null)
            throw new InvalidOperationException("Verification already used.");

        UsedOnUtc = DateTime.UtcNow;
    }

    public bool IsUsed()
    {
        return UsedOnUtc.HasValue;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow >= ExpiresOnUtc;
    }

    public bool IsValid()
    {
        return !IsUsed() && !IsExpired();
    }
}