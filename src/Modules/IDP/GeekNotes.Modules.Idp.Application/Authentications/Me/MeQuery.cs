using BuildingBlocks.User.Abstractions;
using GeekNotes.BuildingBlocks.Application;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.Me;

public sealed record MeQuery()
    : IRequest<OperationResult<MeQueryResponse>>;


internal sealed class MeQueryHandler
    : IRequestHandler<
        MeQuery,
        OperationResult<MeQueryResponse>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;

    public MeQueryHandler(
        IUserService userService, ICurrentUser currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    public async Task<
        OperationResult<MeQueryResponse>>
        Handle(
            MeQuery request,
            CancellationToken cancellationToken)
    {
        var user =
            await _userService.GetByIdAsync(
                _currentUser.UserId,
                cancellationToken);

        if (user is null)
        {
            return OperationResult<
                MeQueryResponse>
                .NotFound("User not found.");
        }

        return OperationResult<
            MeQueryResponse>
            .Success(
                new MeQueryResponse(
                    user.Id,
                    user.Email,
                    user.PhoneNumber,
                    user.UserName,
                    user.FullName,
                    user.Avatar,
                    user.JoinedOnUtc,
                    user.Roles));
    }
}