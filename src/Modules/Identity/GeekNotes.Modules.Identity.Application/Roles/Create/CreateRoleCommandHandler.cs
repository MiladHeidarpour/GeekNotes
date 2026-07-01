namespace GeekNotes.Modules.Identity.Application.Roles.Create;

internal sealed class CreateRoleCommandHandler(IRoleRepository repository)
    : IRequestHandler<CreateRoleCommand, OperationResult<Guid>>
{
    public async Task<OperationResult<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleId = RoleId.CreateUniqueId();
        var roleName = RoleName.Create(request.Name);
        if (await repository.ExistsByNameAsync(roleName, cancellationToken))
        {
            return OperationResult<Guid>.Conflict("نقشی با این عنوان از قبل وجود دارد.");
        }

        var permissions = request.Permissions.Select(Permission.Create)
                                             .Distinct()
                                             .ToList();
        
        var role = Role.Create(roleName, request.Title, permissions);
        repository.Add(role);
        await repository.SaveChangesAsync(cancellationToken);
        return OperationResult<Guid>.Success(Guid.Parse(role.Id.ToString()));
    }
}