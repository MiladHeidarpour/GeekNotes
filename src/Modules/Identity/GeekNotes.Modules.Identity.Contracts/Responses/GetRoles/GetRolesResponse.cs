namespace GeekNotes.Modules.Identity.Contracts.Responses.GetRoles;

public sealed record GetRolesResponse(
    Guid RoleId,
    string Name,
    string Title,
    IReadOnlyCollection<string> Permissions);
