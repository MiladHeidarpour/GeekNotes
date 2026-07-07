namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeSession;

public sealed record RevokeSessionCommand(
    Guid SessionId)
    : IRequest<OperationResult<RevokeSessionCommandResponse>>;
