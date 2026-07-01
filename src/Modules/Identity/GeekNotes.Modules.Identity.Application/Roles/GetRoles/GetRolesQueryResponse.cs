namespace GeekNotes.Modules.Identity.Application.Roles.GetRoles;

public sealed record GetRolesQueryResponse(
    RoleId RoleId,
    RoleName RoleName,
    string Title,
    IReadOnlyCollection<Permission> Permissions)
{
    public static explicit operator GetRolesQueryResponse(Role role)
        => new GetRolesQueryResponse(role.Id,
                                     role.Name,
                                     role.Title,
                                     role.Permissions);
}