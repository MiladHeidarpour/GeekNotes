namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.GetSessions;

public sealed record GetSessionsResponse(
    Guid SessionId,
    string? Device,
    string? UserAgent,
    string? IpAddress,
    DateTime CreatedOnUtc,
    DateTime ExpiresOnUtc,
    bool IsCurrent,
    bool IsRevoked);
