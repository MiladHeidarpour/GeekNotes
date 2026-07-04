namespace GeekNotes.Modules.Identity.Contracts.Requests.Create;

public sealed record CreateRoleRequest(string Name,
                                       string Title,
                                       List<string> Permissions);
