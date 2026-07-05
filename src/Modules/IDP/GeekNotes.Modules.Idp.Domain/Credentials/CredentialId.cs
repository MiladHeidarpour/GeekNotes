using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Credentials;

public sealed class CredentialId : ValueObject<CredentialId>
{
    public Guid Value { get; init; }

    private CredentialId()
    {
    }

    public static CredentialId CreateUnique()
        => new()
        {
            Value = Guid.NewGuid()
        };

    public static CredentialId Create(Guid value)
        => new()
        {
            Value = value
        };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value.ToString();
}
