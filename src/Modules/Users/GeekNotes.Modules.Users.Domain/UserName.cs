using GeekNotes.BuildingBlocks.Domain;
using GeekNotes.Modules.Users.Domain.Exceptions;

namespace GeekNotes.Modules.Users.Domain;

public sealed class UserName : ValueObject<UserName>
{
    public string Value { get; private set; }

    private UserName(string value)
    {
        Value = value;
    }

    public static UserName Create(string value)
    {
        InvalidUserNameException.Throw(value);
        return new(value.Trim().ToLowerInvariant());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value;
}
