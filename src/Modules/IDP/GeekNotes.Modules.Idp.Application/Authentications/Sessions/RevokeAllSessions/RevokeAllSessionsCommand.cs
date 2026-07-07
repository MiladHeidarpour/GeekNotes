namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeAllSessions;

public sealed record RevokeAllSessionsCommand
    : IRequest<OperationResult<RevokeAllSessionsCommandResponse>>;
