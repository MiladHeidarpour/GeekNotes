namespace GeekNotes.Modules.Idp.Contracts.VerifyEmail;

public sealed record VerifyEmailRequest(
    string Email,
    string Code);
