namespace GeekNotes.Modules.Idp.Application.Authentications.Login;

internal sealed class LoginCommandHandler
    : IRequestHandler<LoginCommand, OperationResult<LoginCommandResponse>>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ISessionRepository _sessionRepository;

    public LoginCommandHandler(
        ICredentialRepository credentialRepository,
        IUserService userService,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        ISessionRepository sessionRepository)
    {
        _credentialRepository = credentialRepository;
        _userService = userService;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _sessionRepository = sessionRepository;
    }

    public async Task<OperationResult<LoginCommandResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = _userService.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return OperationResult<LoginCommandResponse>
                .NotFound("NotFound User!");
        }
        var userId = Guid.Parse(user.Id.ToString());

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

        var accessToken = _jwtTokenGenerator.GenerateAccessToken(
            userId,
            session.Id.Value,
            request.Email,
            user.Result.Roles,
            []);


        return OperationResult<LoginCommandResponse>.Success(
            new LoginCommandResponse(
                accessToken,
                refreshToken,
                DateTime.UtcNow.AddMinutes(30)));
    }
}