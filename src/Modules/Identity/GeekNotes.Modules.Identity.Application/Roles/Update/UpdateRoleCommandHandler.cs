namespace GeekNotes.Modules.Identity.Application.Roles.Update;

internal sealed class UpdateRoleCommandHandler(IRoleRepository repository) 
    : IRequestHandler<UpdateRoleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await repository.GetByIdAsync(request.RoleId, cancellationToken);

        if (role is null)
            return OperationResult.NotFound();

        var permissions = request.Permissions.Select(Permission.Create)
                                             .Distinct()
                                             .ToList();

        role.UpdateTitle(request.Title); role.SetPermissions(permissions);

        await repository.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}