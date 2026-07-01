using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Identity.Domain.Roles;

public class Permission : ValueObject<Permission>
{
    public string Name { get; }

    private Permission(string name)
    {
        Name = name;
    }

    public static Permission Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Permission is required.");

        var normalized = name
            .Trim()
            .ToUpperInvariant();

        if (normalized.Length > 128)
            throw new ArgumentException("Permission is too long.");

        return new(normalized);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString()
    {
        return Name;
    }
}