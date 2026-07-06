using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Idp.Domain.Sessions;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.Logout;

public sealed record LogoutCommand(
    string RefreshToken)
    : IRequest<OperationResult<LogoutCommandResponse>>;

//public sealed class LogoutCommandValidator
//    : AbstractValidator<LogoutCommand>
//{
//    public LogoutCommandValidator()
//    {
//        RuleFor(x => x.RefreshToken)
//            .NotEmpty();
//    }
//}

internal sealed class LogoutCommandHandler
    : IRequestHandler<
        LogoutCommand,
        OperationResult<LogoutCommandResponse>>
{
    private readonly ISessionRepository _sessionRepository;

    public LogoutCommandHandler(
        ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<
        OperationResult<LogoutCommandResponse>>
        Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
    {
        var hash =
            RefreshTokenHash.Create(request.RefreshToken);

        var session =
            await _sessionRepository
                .GetByRefreshTokenAsync(
                    hash,
                    cancellationToken);

        if (session is null)
        {
            return OperationResult<
                LogoutCommandResponse>
                .Error("Session not found.");
        }

        if (session.IsRevoked())
        {
            return OperationResult<
                LogoutCommandResponse>
                .Success(new(true));
        }

        session.Revoke();

        await _sessionRepository.UpdateAsync(
            session,
            cancellationToken);

        return OperationResult<
            LogoutCommandResponse>
            .Success(new(true));
    }
}