using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Idp.Domain;
using GeekNotes.Modules.Idp.Domain.Credentials;
using GeekNotes.Modules.Idp.Domain.Sessions;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.Login;

//public sealed class LoginCommandValidator
//    : AbstractValidator<LoginCommand>
//{
//    public LoginCommandValidator()
//    {
//        RuleFor(x => x.Email)
//            .NotEmpty()
//            .EmailAddress();

//        RuleFor(x => x.Password)
//            .NotEmpty();
//    }
//}

internal sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand,
        OperationResult<LoginCommandResponse>>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ISessionRepository _sessionRepository;

    public LoginCommandHandler(
        ICredentialRepository credentialRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        ISessionRepository sessionRepository)
    {
        _credentialRepository = credentialRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _sessionRepository = sessionRepository;
    }

    public async Task<OperationResult<LoginCommandResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // TODO
        // IUserService.GetByEmail()
        var userIdguid = Guid.Parse("b55e04ac-d17d-452a-a605-b2625a8386c2");
        Guid userId = userIdguid;

        var credential = await _credentialRepository.GetByUserIdAsync(
            userId,
            cancellationToken);

        if (credential is null)
        {
            return OperationResult<LoginCommandResponse>
                .Error("Invalid email or password.");
        }

        if (credential.IsLocked())
        {
            return OperationResult<LoginCommandResponse>
                .Error("Account is locked.");
        }

        var verifyResult = _passwordHasher.Verify(
            credential.PasswordHash,
            request.Password);

        if (verifyResult == PasswordVerificationStatus.Failed)
        {
            credential.RegisterFailedAttempt();

            if (credential.FailedAccessCount >= 5)
            {
                credential.LockUntil(
                    DateTime.UtcNow.AddMinutes(15));
            }

            return OperationResult<LoginCommandResponse>
                .Error("Invalid email or password.");
        }

        credential.ResetFailedAttempts();

        if (verifyResult == PasswordVerificationStatus.SuccessRehashNeeded)
        {
            credential.ChangePassword(
                _passwordHasher.Hash(request.Password));
        }

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(
            userId,
            request.Email,
            [],
            []);

        var refreshToken =
            _refreshTokenGenerator.Generate();

        var session = Session.Create(
            userId,
            RefreshTokenHash.Create(refreshToken),
            DeviceId.Create(Guid.NewGuid().ToString()),
            null,
            null,
            DateTime.UtcNow.AddDays(30));

        await _sessionRepository.AddAsync(
            session,
            cancellationToken);

        await _sessionRepository.SaveChangesAsync(cancellationToken);

        return OperationResult<LoginCommandResponse>.Success(
            new LoginCommandResponse(
                accessToken,
                refreshToken,
                DateTime.UtcNow.AddMinutes(30)));
    }
}