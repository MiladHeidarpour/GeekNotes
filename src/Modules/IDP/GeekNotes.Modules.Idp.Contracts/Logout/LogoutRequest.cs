namespace GeekNotes.Modules.Idp.Contracts.Logout;

public sealed record LogoutRequest(
    string RefreshToken);
