namespace GeekNotes.Modules.Identity.Application.Roles.Delete;

internal sealed class DeleteRoleCommandHandler(IRoleRepository repository)
    : IRequestHandler<DeleteRoleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await repository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
        {
            return OperationResult.NotFound("Role not found.");
        }
        repository.Delete(role);
        return OperationResult.Success();
    }
}