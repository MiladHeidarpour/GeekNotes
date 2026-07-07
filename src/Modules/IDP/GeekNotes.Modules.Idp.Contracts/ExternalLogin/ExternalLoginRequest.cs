namespace GeekNotes.Modules.Idp.Contracts.ExternalLogin;

public sealed record ExternalLoginRequest(
    ExternalProvider Provider,
    string Token);
