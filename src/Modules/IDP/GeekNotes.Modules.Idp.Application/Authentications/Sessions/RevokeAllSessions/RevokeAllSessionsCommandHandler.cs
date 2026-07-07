namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeAllSessions;

internal sealed class RevokeAllSessionsCommandHandler
    : IRequestHandler<
        RevokeAllSessionsCommand,
        OperationResult<RevokeAllSessionsCommandResponse>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICurrentUser _currentUser;

    public RevokeAllSessionsCommandHandler(
        ISessionRepository sessionRepository,
        ICurrentUser currentUser)
    {
        _sessionRepository = sessionRepository;
        _currentUser = currentUser;
    }

    public async Task<OperationResult<RevokeAllSessionsCommandResponse>> Handle(
        RevokeAllSessionsCommand request,
        CancellationToken cancellationToken)
    {
        await _sessionRepository.RevokeAllAsync(
            _currentUser.UserId,
            cancellationToken);

        return OperationResult<RevokeAllSessionsCommandResponse>
            .Success(new(true));
    }
}