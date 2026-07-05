using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Verifications;

public sealed class VerificationCode : ValueObject<VerificationCode>
{
    public string Value { get; }

    private VerificationCode(string value)
    {
        Value = value;
    }

    public static VerificationCode Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > 20)
            throw new ArgumentException("Verification code is invalid.");

        return new VerificationCode(value.Trim());
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