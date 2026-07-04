namespace GeekNotes.Modules.Identity.Contracts.Requests.GetRoles;

public sealed record GetRolesRequest(
     int Page = 1,
     int Size = 2,
     string Title = "");
