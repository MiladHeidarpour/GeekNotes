namespace GeekNotes.Modules.Idp.Contracts.Token;

public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    int ExpiresIn);
