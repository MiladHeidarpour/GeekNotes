namespace GeekNotes.Modules.Identity.Application.Roles.GetRoles;

internal sealed class GetRolesQueryHandler(IRoleRepository repository)
    : IRequestHandler<GetRolesQuery, OperationResult<PaginatedList<GetRolesQueryResponse>>>
{
    public async Task<OperationResult<PaginatedList<GetRolesQueryResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {

        int totalCount = await repository.CountRolesAsync(request.Title, cancellationToken);
        var roles = await repository.GetRolesAsync(request.PageNumber, request.PageSize, request.Title, cancellationToken);
        var items = roles.Select(x => (GetRolesQueryResponse)x).ToArray();

        if (items.Length == 0)
            return OperationResult<PaginatedList<GetRolesQueryResponse>>.NotFound("نقشی یافت نشد.");

        var paginatedList = new PaginatedList<GetRolesQueryResponse>
        {
            Items = items,
            PageNumber = request.PageNumber,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        return OperationResult<PaginatedList<GetRolesQueryResponse>>.Success(paginatedList);
    }
}