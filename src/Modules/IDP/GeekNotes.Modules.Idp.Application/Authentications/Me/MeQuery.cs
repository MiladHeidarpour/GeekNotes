namespace GeekNotes.Modules.Idp.Application.Authentications.Me;

public sealed record MeQuery()
    : IRequest<OperationResult<MeQueryResponse>>;
