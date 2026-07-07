namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.GetSessions;

public sealed record GetSessionsQuery
    : IRequest<OperationResult<IReadOnlyList<GetSessionsResponse>>>;
