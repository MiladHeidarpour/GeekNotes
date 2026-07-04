namespace GeekNotes.Modules.Users.Contracts.Requests.Create;

public sealed record CreateUserRequest(
    string UserName,
    string FullName,
    string Email,
    string PhoneNumber,
    string PasswordHash,
    List<Guid> RoleIds);
