namespace GeekNotes.Modules.Idp.Contracts.Authorize;

public sealed record AuthorizeRequest(
    string ClientId,
    string RedirectUri,
    string ResponseType,
    string Scope,
    string State,
    string? CodeChallenge,
    string? CodeChallengeMethod);
