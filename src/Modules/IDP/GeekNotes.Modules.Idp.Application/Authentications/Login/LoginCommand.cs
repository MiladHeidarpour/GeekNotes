namespace GeekNotes.Modules.Idp.Application.Authentications.Login;

public sealed record LoginCommand(
    string Email,
    string Password)
    : IRequest<OperationResult<LoginCommandResponse>>;
