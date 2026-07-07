namespace GeekNotes.Modules.Idp.Contracts.RevokeSession;

public sealed record RevokeSessionRequest(
    Guid SessionId);
