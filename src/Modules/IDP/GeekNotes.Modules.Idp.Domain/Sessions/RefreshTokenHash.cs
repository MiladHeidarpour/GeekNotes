using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Sessions;

public sealed class RefreshTokenHash : ValueObject<RefreshTokenHash>
{
    public string Value { get; }

    private RefreshTokenHash(string value)
    {
        Value = value;
    }

    public static RefreshTokenHash Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new RefreshTokenHash(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}