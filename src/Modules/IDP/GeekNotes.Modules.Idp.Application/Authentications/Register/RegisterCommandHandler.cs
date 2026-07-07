namespace GeekNotes.Modules.Idp.Application.Authentications.Register;

internal sealed class RegisterCommandHandler
    : IRequestHandler<
        RegisterCommand,
        OperationResult<RegisterCommandResponse>>
{
    private readonly ICredentialRepository _credentialRepository;
    private readonly IVerificationRepository _verificationRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        ICredentialRepository credentialRepository,
        IVerificationRepository verificationRepository,
        IPasswordHasher passwordHasher)
    {
        _credentialRepository = credentialRepository;
        _verificationRepository = verificationRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<OperationResult<RegisterCommandResponse>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        // TODO:
        // Create User Module

        Guid userId = Guid.NewGuid();

        if (await _credentialRepository.ExistsAsync(userId, cancellationToken))
        {
            return OperationResult<RegisterCommandResponse>
                .Conflict("Credential already exists.");
        }

        var credential = Credential.Create(
            userId,
            _passwordHasher.Hash(request.Password));

        await _credentialRepository.AddAsync(
            credential,
            cancellationToken);
        await _credentialRepository.SaveChangesAsync(cancellationToken);

        var verification = Verification.Create(
            userId,
            VerificationType.Email,
            VerificationCode.Create(Random.Shared.Next(100000, 999999).ToString()),
            DateTime.UtcNow.AddMinutes(5));

        await _verificationRepository.AddAsync(
            verification,
            cancellationToken);

        await _verificationRepository.SaveChangesAsync(cancellationToken);

        return OperationResult<RegisterCommandResponse>.Success(
            new RegisterCommandResponse(
                userId,
                request.Email));
    }
}