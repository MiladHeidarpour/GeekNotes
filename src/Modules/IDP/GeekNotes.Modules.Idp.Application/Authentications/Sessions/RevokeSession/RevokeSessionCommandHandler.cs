namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeSession;

internal sealed class RevokeSessionCommandHandler
    : IRequestHandler<
        RevokeSessionCommand,
        OperationResult<RevokeSessionCommandResponse>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICurrentUser _currentUser;

    public RevokeSessionCommandHandler(
        ISessionRepository sessionRepository,
        ICurrentUser currentUser)
    {
        _sessionRepository = sessionRepository;
        _currentUser = currentUser;
    }

    public async Task<OperationResult<RevokeSessionCommandResponse>> Handle(
        RevokeSessionCommand request,
        CancellationToken cancellationToken)
    {
        var session =
            await _sessionRepository.GetByIdAsync(
                SessionId.Create(request.SessionId),
                cancellationToken);

        if (session is null)
            return OperationResult<RevokeSessionCommandResponse>
                .NotFound("Session not found.");

        if (session.UserId != _currentUser.UserId)
            return OperationResult<RevokeSessionCommandResponse>
                .Error("Access denied.");

        if (!session.IsRevoked())
        {
            session.Revoke();

            await _sessionRepository.UpdateAsync(
                session,
                cancellationToken);

            await _sessionRepository.SaveChangesAsync(cancellationToken);
        }

        return OperationResult<RevokeSessionCommandResponse>
            .Success(new(true));
    }
}