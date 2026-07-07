namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.GetSessions;

internal sealed class GetSessionsQueryHandler
    : IRequestHandler<
        GetSessionsQuery,
        OperationResult<IReadOnlyList<GetSessionsResponse>>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICurrentUser _currentUser;

    public GetSessionsQueryHandler(
        ISessionRepository sessionRepository,
        ICurrentUser currentUser)
    {
        _sessionRepository = sessionRepository;
        _currentUser = currentUser;
    }

    public async Task<
        OperationResult<IReadOnlyList<GetSessionsResponse>>>
        Handle(
            GetSessionsQuery request,
            CancellationToken cancellationToken)
    {
        var sessions =
            await _sessionRepository
                .GetByUserIdAsync(
                   _currentUser.UserId,
                    cancellationToken);

        var response =
            sessions
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x =>
                    new GetSessionsResponse(
                        x.Id.Value,
                        x.DeviceId?.Value,
                        x.UserAgent,
                        x.IpAddress,
                        x.CreatedOnUtc,
                        x.ExpiresOnUtc,
                        IsCurrent: x.Id.Value == _currentUser.SessionId,
                        x.IsRevoked()))
                .ToList();

        return OperationResult<
            IReadOnlyList<GetSessionsResponse>>
            .Success(response);
    }
}