using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.ExternalLogins;

public sealed class ExternalLoginId : ValueObject<ExternalLoginId>
{
    public Guid Value { get; init; }

    private ExternalLoginId()
    {
    }

    public static ExternalLoginId CreateUnique()
        => new()
        {
            Value = Guid.NewGuid()
        };

    public static ExternalLoginId Create(Guid value)
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