using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Idp.Domain;
using GeekNotes.Modules.Idp.Domain.Credentials;
using GeekNotes.Modules.Idp.Domain.Verifications;
using MediatR;

namespace GeekNotes.Modules.Idp.Application.Authentications.Register;

//public sealed class RegisterCommandValidator
//    : AbstractValidator<RegisterCommand>
//{
//    public RegisterCommandValidator()
//    {
//        RuleFor(x => x.Email)
//            .NotEmpty()
//            .EmailAddress();

//        RuleFor(x => x.Password)
//            .NotEmpty()
//            .MinimumLength(8)
//            .MaximumLength(100);

//        RuleFor(x => x.FirstName)
//            .NotEmpty()
//            .MaximumLength(100);

//        RuleFor(x => x.LastName)
//            .NotEmpty()
//            .MaximumLength(100);
//    }
//}

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