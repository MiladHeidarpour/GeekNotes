namespace GeekNotes.Modules.Idp.Contracts.Login;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc);
