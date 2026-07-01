using GeekNotes.BuildingBlocks.Domain;
using GeekNotes.Modules.Users.Domain.Exceptions;

namespace GeekNotes.Modules.Users.Domain;

public sealed class Email : ValueObject<Email>
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        InvalidEmailAddressException.Throw(value);
        return new(value.Trim().ToLowerInvariant());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value;
}
