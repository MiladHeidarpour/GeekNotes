namespace GeekNotes.Modules.Idp.Application.Authentications.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : IRequest<OperationResult<RefreshTokenCommandResponse>>;
