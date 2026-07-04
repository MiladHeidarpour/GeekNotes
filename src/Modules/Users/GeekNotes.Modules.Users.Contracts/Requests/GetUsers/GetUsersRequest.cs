namespace GeekNotes.Modules.Users.Contracts.Requests.GetUsers;

public sealed record GetUsersRequest(
    int Page,
    int Size,
    string? FullName);
