namespace GeekNotes.Modules.Idp.Application.Authentications.Logout;

public sealed record LogoutCommand(
    string RefreshToken)
    : IRequest<OperationResult<LogoutCommandResponse>>;
