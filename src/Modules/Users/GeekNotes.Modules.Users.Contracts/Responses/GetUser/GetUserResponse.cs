namespace GeekNotes.Modules.Users.Contracts.Responses.GetUser;

public sealed record GetUserResponse(
    Guid UserId,
    string UserName,
    string FullName,
    string Email,
    string PhoneNumber,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<Guid> Roles);