namespace GeekNotes.Modules.Idp.Contracts.Introspection;

public sealed record IntrospectionResponse(
    bool Active,
    Guid? UserId,
    string? ClientId,
    DateTime? ExpiresOnUtc,
    IEnumerable<string>? Scopes);
