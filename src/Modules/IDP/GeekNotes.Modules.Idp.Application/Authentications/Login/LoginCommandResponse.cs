namespace GeekNotes.Modules.Idp.Application.Authentications.Login;

public sealed record LoginCommandResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc);