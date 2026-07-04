namespace GeekNotes.Modules.Identity.Contracts.Requests.Update;

public sealed record UpdateRoleRequest(Guid RoleId, string Title, List<string> Permissions);