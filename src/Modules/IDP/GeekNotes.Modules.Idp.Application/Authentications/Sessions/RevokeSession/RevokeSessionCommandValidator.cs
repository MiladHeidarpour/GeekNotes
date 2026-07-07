namespace GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeSession;

public sealed class RevokeSessionCommandValidator
    : AbstractValidator<RevokeSessionCommand>
{
    public RevokeSessionCommandValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty();
    }
}
