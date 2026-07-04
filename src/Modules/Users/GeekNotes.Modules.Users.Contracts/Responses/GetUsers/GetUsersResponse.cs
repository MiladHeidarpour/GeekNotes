namespace GeekNotes.Modules.Users.Contracts.Responses.GetUsers;

public sealed record GetUsersResponse(
    string UserId,
    string UserName,
    string FullName,
    string Email,
    string PhoneNumber,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<Guid> Roles);
