using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Identity.Domain.Roles;

public sealed class RoleName : ValueObject<RoleName>
{
    public string Value { get; private set; }

    private RoleName(string value)
    {
        Value = value;
    }

    public static RoleName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Role name is required.");

        var normalized = value
            .Trim()
            .ToUpperInvariant();

        if (normalized.Length > 64)
            throw new ArgumentException("Role name is too long.");

        return new(normalized);
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