namespace GeekNotes.Modules.Identity.Application.Roles.Update;

public sealed record UpdateRoleCommand(RoleId RoleId, string Title, List<string> Permissions)
    : IRequest<OperationResult>;
