namespace GeekNotes.Modules.Idp.Contracts.Login;

public sealed record LoginRequest(
    string Email,
    string Password);
