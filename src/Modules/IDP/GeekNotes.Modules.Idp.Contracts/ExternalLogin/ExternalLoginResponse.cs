namespace GeekNotes.Modules.Idp.Contracts.ExternalLogin;

public sealed record ExternalLoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc,
    bool RequiresPhoneNumber,
    bool RequiresEmailVerification);
