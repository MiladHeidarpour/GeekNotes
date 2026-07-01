namespace GeekNotes.Modules.Users.Application.GetUser;

public sealed class GetUserQueryHandler(IUserRepository repository)
    : IRequestHandler<GetUserQuery, OperationResult<GetUserQueryResponse?>>
{
    public async Task<OperationResult<GetUserQueryResponse?>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return OperationResult<GetUserQueryResponse>.NotFound();

        return OperationResult<GetUserQueryResponse>.Success((GetUserQueryResponse)user);
    }
}
