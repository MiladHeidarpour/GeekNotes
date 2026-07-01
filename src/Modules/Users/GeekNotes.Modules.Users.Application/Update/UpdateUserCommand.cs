namespace GeekNotes.Modules.Users.Application.Update;

public sealed record UpdateUserCommand(
    UserId UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    IReadOnlyList<Guid> RoleIds)
    : IRequest<OperationResult>;
