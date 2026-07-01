namespace GeekNotes.Modules.Identity.Application.Roles.GetRole;

internal sealed class GetRoleQueryHandler(IRoleRepository repository) 
    : IRequestHandler<GetRoleQuery, OperationResult<GetRoleQueryResponse>>
{
    public async Task<OperationResult<GetRoleQueryResponse>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await repository.GetByIdAsync(request.RoleId,cancellationToken);

        if (role is null)
            return OperationResult<GetRoleQueryResponse>.NotFound();

        return OperationResult<GetRoleQueryResponse>.Success((GetRoleQueryResponse)role);
    }
}