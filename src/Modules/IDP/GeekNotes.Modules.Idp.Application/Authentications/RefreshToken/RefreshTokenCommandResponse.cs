namespace GeekNotes.Modules.Idp.Application.Authentications.RefreshToken;

public sealed record RefreshTokenCommandResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc);