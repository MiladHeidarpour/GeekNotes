
using GeekNotes.Modules.Users.Domain;

namespace GeekNotes.Modules.Users.Application.Create;

public sealed record CreateUserCommandResponse(
    UserId UserId);