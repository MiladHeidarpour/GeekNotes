namespace GeekNotes.Modules.Idp.Contracts.Token;

public sealed record TokenRequest(
    string GrantType,
    string ClientId,
    string? ClientSecret,
    string? Code,
    string? RedirectUri,
    string? CodeVerifier,
    string? RefreshToken);
