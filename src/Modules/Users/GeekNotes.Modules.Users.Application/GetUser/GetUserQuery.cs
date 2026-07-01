namespace GeekNotes.Modules.Users.Application.GetUser;

public sealed record GetUserQuery(UserId UserId) : IRequest<OperationResult<GetUserQueryResponse?>>;
