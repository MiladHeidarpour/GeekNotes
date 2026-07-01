using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Users.Domain;

public sealed class UserId : ValueObject<UserId>
{
    public Guid Value { get; private set; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value)
        => new(value);

    public static UserId CreateUnique()
        => new(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
        => Value.ToString();
}
