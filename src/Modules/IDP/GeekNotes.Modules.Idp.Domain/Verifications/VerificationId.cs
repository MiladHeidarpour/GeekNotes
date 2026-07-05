using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Verifications;

public sealed class VerificationId : ValueObject<VerificationId>
{
    public Guid Value { get; init; }

    private VerificationId()
    {
    }

    public static VerificationId CreateUnique()
        => new()
        {
            Value = Guid.NewGuid()
        };

    public static VerificationId Create(Guid value)
        => new()
        {
            Value = value
        };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
