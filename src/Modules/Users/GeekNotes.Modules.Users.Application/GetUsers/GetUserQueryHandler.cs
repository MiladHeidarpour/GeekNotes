namespace GeekNotes.Modules.Users.Application.GetUsers;

public sealed class GetUserQueryHandler(IUserRepository repository)
    : IRequestHandler<GetUsersQuery, OperationResult<PaginatedList<GetUsersQueryResponse>>>
{
    public async Task<OperationResult<PaginatedList<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        int totalCount = await repository.CountUsersAsync(cancellationToken);
        var roles = await repository.GetUsersAsync(request.PageNumber, request.PageSize, request.FullName, cancellationToken);
        var items = roles.Select(x => (GetUsersQueryResponse)x).ToArray();

        if (items.Length == 0)
            return OperationResult<PaginatedList<GetUsersQueryResponse>>.NotFound("کاربری یافت نشد.");

        var paginatedList = new PaginatedList<GetUsersQueryResponse>
        {
            Items = items,
            PageNumber = request.PageNumber,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        return OperationResult<PaginatedList<GetUsersQueryResponse>>.Success(paginatedList);
    }
}