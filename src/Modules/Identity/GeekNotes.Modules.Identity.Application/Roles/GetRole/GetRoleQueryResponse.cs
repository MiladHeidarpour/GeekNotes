namespace GeekNotes.Modules.Identity.Application.Roles.GetRole;

public sealed record GetRoleQueryResponse(
    RoleId RoleId,
    RoleName RoleName,
    string Title,
    IReadOnlyCollection<Permission> Permissions)
{
    public static explicit operator GetRoleQueryResponse(Role role) 
        => new GetRoleQueryResponse(role.Id,
                                    role.Name,
                                    role.Title,
                                    role.Permissions);
}