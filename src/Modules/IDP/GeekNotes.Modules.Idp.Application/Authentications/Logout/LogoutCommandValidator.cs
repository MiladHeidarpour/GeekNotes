namespace GeekNotes.Modules.Idp.Application.Authentications.Logout;

public sealed class LogoutCommandValidator
    : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
