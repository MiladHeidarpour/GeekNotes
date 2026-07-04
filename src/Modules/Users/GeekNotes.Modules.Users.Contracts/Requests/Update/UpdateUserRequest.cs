namespace GeekNotes.Modules.Users.Contracts.Requests.Update;

public sealed record UpdateUserRequest(
    Guid UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    IReadOnlyList<Guid> RoleIds);