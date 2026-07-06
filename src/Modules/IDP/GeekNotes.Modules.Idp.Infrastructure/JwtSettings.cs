namespace GeekNotes.Modules.Idp.Infrastructure;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";

    public string Issuer { get; init; } = default!;

    public string Audience { get; init; } = default!;

    public int ExpiryMinutes { get; init; }

    public string PrivateKey { get; init; } = default!;
}
