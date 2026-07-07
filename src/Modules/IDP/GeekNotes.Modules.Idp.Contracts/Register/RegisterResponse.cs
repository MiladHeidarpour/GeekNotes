namespace GeekNotes.Modules.Idp.Contracts.Register;

public sealed record RegisterResponse(
    Guid UserId,
    string Email);
