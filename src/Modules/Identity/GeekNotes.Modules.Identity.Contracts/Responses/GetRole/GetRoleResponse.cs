namespace GeekNotes.Modules.Identity.Contracts.Responses.GetRole;

public sealed record GetRoleResponse(
    Guid RoleId,
    string Name,
    string Title,
    IReadOnlyCollection<string> Permissions);
