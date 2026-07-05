using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.ExternalLogins;

public sealed class ExternalLogin : AggregateRoot<ExternalLoginId>
{
    private ExternalLogin()
        : base(null!)
    {
    }

    private ExternalLogin(
        ExternalLoginId id,
        Guid userId,
        ExternalProvider provider,
        string providerUserId,
        string email)
        : base(id)
    {
        UserId = userId;
        Provider = provider;
        ProviderUserId = providerUserId;
        Email = email;

        LinkedOnUtc = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public ExternalProvider Provider { get; private set; }

    public string ProviderUserId { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public DateTime LinkedOnUtc { get; private set; }

    public static ExternalLogin Create(
        Guid userId,
        ExternalProvider provider,
        string providerUserId,
        string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(providerUserId);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        return new ExternalLogin(
            ExternalLoginId.CreateUnique(),
            userId,
            provider,
            providerUserId.Trim(),
            email.Trim().ToLowerInvariant());
    }

    public bool IsGoogle()
        => Provider == ExternalProvider.Google;

    public bool IsGitHub()
        => Provider == ExternalProvider.GitHub;

    public bool IsMicrosoft()
        => Provider == ExternalProvider.Microsoft;

    public bool IsApple()
        => Provider == ExternalProvider.Apple;

    public bool IsDiscord()
        => Provider == ExternalProvider.Discord;

    public bool IsTelegram()
        => Provider == ExternalProvider.Telegram;
}
