
namespace GeekNotes.Modules.Users.Application.Delete;

public sealed record DeleteUserCommand(UserId UserId) : IRequest<OperationResult>;
