namespace GeekNotes.Modules.Idp.Application.Authentications.RefreshToken;

public sealed class RefreshTokenCommandValidator
    : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
