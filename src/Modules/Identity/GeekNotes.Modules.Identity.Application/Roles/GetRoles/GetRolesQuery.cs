namespace GeekNotes.Modules.Identity.Application.Roles.GetRoles;

public sealed record GetRolesQuery(int PageNumber = 1, int PageSize = 10, string Title = "")
    : IRequest<OperationResult<PaginatedList<GetRolesQueryResponse>>>;
