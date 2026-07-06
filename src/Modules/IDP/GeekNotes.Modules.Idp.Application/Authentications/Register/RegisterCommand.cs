using GeekNotes.BuildingBlocks.Application;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName)
    : IRequest<OperationResult<RegisterCommandResponse>>;
