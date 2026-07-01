using GeekNotes.BuildingBlocks.Domain;
using GeekNotes.Modules.Users.Domain.Exceptions;

namespace GeekNotes.Modules.Users.Domain;

public sealed class PhoneNumber : ValueObject<PhoneNumber>
{
    public string Value { get; private set; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string value)
    {
        InvalidPhoneNumberException.Throw(value);
        return new(value.Trim());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value;
}
