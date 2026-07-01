using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Identity.Domain.Roles;

public sealed class RoleId : ValueObject<RoleId>
{
    public required Guid Value { get; init; }

    public static RoleId CreateUniqueId()
        => new RoleId { Value = Guid.NewGuid() };

    public static RoleId Create(Guid roleId)
        => new RoleId { Value = roleId };

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
