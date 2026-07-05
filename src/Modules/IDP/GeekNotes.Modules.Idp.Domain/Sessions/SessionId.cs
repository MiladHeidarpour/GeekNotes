using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Idp.Domain.Sessions;

public sealed class SessionId : ValueObject<SessionId>
{
    public Guid Value { get; init; }

    private SessionId()
    {
    }

    public static SessionId CreateUnique()
        => new()
        {
            Value = Guid.NewGuid()
        };

    public static SessionId Create(Guid value)
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