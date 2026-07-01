namespace GeekNotes.Modules.Users.Application.Create;

public sealed record CreateUserCommand(
    string UserName,
    string FullName,
    string Email,
    string PhoneNumber,
    string PasswordHash,
    IReadOnlyList<Guid> RoleIds)
    : IRequest<OperationResult<CreateUserCommandResponse>>;
