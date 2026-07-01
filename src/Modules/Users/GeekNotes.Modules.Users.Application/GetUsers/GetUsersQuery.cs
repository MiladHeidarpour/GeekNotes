namespace GeekNotes.Modules.Users.Application.GetUsers;

public sealed record GetUsersQuery(
    int PageNumber,
    int PageSize,
    string? FullName)
    : IRequest<OperationResult<PaginatedList<GetUsersQueryResponse>>>;