namespace GeekNotes.Modules.Identity.Application.Roles.Create;

public sealed record CreateRoleCommand(string Name, string Title, List<string> Permissions)
    : IRequest<OperationResult<Guid>>;