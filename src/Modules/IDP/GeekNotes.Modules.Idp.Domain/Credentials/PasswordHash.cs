using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Credentials;

public sealed class PasswordHash : ValueObject<PasswordHash>
{
    public string Value { get; init; } = null!;

    private PasswordHash()
    {
    }

    public static PasswordHash Create(string hash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(hash);

        return new PasswordHash
        {
            Value = hash
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value;
}