namespace GeekNotes.Modules.Identity.Application.Roles.Delete;

public sealed record DeleteRoleCommand(RoleId RoleId)
    : IRequest<OperationResult>;
