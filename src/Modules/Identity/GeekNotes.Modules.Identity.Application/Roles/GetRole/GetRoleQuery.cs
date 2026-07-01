namespace GeekNotes.Modules.Identity.Application.Roles.GetRole;

public sealed record GetRoleQuery(RoleId RoleId)
    : IRequest<OperationResult<GetRoleQueryResponse>>;
