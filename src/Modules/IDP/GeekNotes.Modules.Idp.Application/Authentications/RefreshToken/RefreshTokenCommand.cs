using BuildingBlocks.User.Abstractions;
using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Idp.Domain;
using GeekNotes.Modules.Idp.Domain.Sessions;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : IRequest<OperationResult<RefreshTokenCommandResponse>>;


//public sealed class RefreshTokenCommandValidator
//    : AbstractValidator<RefreshTokenCommand>
//{
//    public RefreshTokenCommandValidator()
//    {
//        RuleFor(x => x.RefreshToken)
//            .NotEmpty();
//    }
//}

internal sealed class RefreshTokenCommandHandler
    : IRequestHandler<
        RefreshTokenCommand,
        OperationResult<RefreshTokenCommandResponse>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUserService _userService;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshGenerator;

    public RefreshTokenCommandHandler(
        ISessionRepository sessionRepository,
        IUserService userService,
        IJwtTokenGenerator jwtGenerator,
        IRefreshTokenGenerator refreshGenerator)
    {
        _sessionRepository = sessionRepository;
        _userService = userService;
        _jwtGenerator = jwtGenerator;
        _refreshGenerator = refreshGenerator;
    }

    public async Task<OperationResult<RefreshTokenCommandResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshTokenHash =
            RefreshTokenHash.Create(request.RefreshToken);

        var session =
            await _sessionRepository
                .GetByRefreshTokenAsync(
                    refreshTokenHash,
                    cancellationToken);

        if (session is null)
        {
            return OperationResult<RefreshTokenCommandResponse>
                .Error("Invalid refresh token.");
        }

        if (session.IsExpired())
        {
            return OperationResult<RefreshTokenCommandResponse>
                .Error("Refresh token expired.");
        }

        if (session.IsRevoked())
        {
            return OperationResult<RefreshTokenCommandResponse>
                .Error("Refresh token revoked.");
        }

        var user =
            await _userService.GetByIdAsync(
                session.UserId,
                cancellationToken);

        if (user is null)
        {
            return OperationResult<RefreshTokenCommandResponse>
                .Error("User not found.");
        }

        //var accessToken =
        //    _jwtGenerator.GenerateAccessToken(
        //        user.Id.Value,
        //        user.Email.Value,
        //        user.Roles.Select(x => x.Name.Value),
        //        user.Roles
        //            .SelectMany(x => x.Permissions)
        //            .Select(x => x.Name));

        var accessToken =
            _jwtGenerator.GenerateAccessToken(
                user.Id,
                user.Email,
                user.Roles,
                user.Roles);

        var refreshToken =
            _refreshGenerator.Generate();

        session.Revoke();

        var newSession =
            Session.Create(
                user.Id,
                RefreshTokenHash.Create(refreshToken),
                session.DeviceId,
                session.IpAddress,
                session.UserAgent,
                DateTime.UtcNow.AddDays(30));

        await _sessionRepository.AddAsync(
            newSession,
            cancellationToken);

        await _sessionRepository.UpdateAsync(
            session,
            cancellationToken);

        return OperationResult<
            RefreshTokenCommandResponse>.Success(
                new RefreshTokenCommandResponse(
                    accessToken,
                    refreshToken,
                    DateTime.UtcNow.AddMinutes(30)));
    }
}