using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Sessions;

public sealed class Session : AggregateRoot<SessionId>
{
    private Session()
        : base(null!)
    {
    }

    private Session(
        SessionId id,
        Guid userId,
        RefreshTokenHash refreshTokenHash,
        DeviceId deviceId,
        string? userAgent,
        string? ipAddress,
        DateTime expiresOnUtc)
        : base(id)
    {
        UserId = userId;
        RefreshTokenHash = refreshTokenHash;
        DeviceId = deviceId;
        UserAgent = userAgent;
        IpAddress = ipAddress;
        ExpiresOnUtc = expiresOnUtc;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public RefreshTokenHash RefreshTokenHash { get; private set; } = null!;

    public DeviceId DeviceId { get; private set; } = null!;

    public string? UserAgent { get; private set; }

    public string? IpAddress { get; private set; }

    public DateTime ExpiresOnUtc { get; private set; }

    public DateTime? RevokedOnUtc { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Session Create(
        Guid userId,
        RefreshTokenHash refreshTokenHash,
        DeviceId deviceId,
        string? userAgent,
        string? ipAddress,
        DateTime expiresOnUtc)
    {
        return new Session(
            SessionId.CreateUnique(),
            userId,
            refreshTokenHash,
            deviceId,
            userAgent,
            ipAddress,
            expiresOnUtc);
    }

    public void Rotate(RefreshTokenHash refreshTokenHash)
    {
        RefreshTokenHash = refreshTokenHash;
    }

    public void Revoke()
    {
        if (RevokedOnUtc is not null)
            return;

        RevokedOnUtc = DateTime.UtcNow;
    }

    public bool IsRevoked()
    {
        return RevokedOnUtc.HasValue;
    }

    public bool IsExpired()
    {
        return ExpiresOnUtc <= DateTime.UtcNow;
    }

    public bool IsActive()
    {
        return !IsExpired() && !IsRevoked();
    }
}