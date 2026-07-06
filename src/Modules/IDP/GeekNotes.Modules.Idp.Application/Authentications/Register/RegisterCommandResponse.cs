namespace GeekNotes.Modules.Idp.Application.Authentications.Register;

public sealed record RegisterCommandResponse(
    Guid UserId,
    string Email);
