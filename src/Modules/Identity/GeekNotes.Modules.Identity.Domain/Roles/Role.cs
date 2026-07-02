using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Identity.Domain.Roles;

public class Role : AggregateRoot<RoleId>
{
    private readonly List<Permission> _permissions = [];
    private Role(RoleId id) : base(id)
    {
    }
    private Role() : this(null!) { }

    public RoleName Name { get; private set; } = null!;

    public string Title { get; private set; } = null!;

    public IReadOnlyCollection<Permission> Permissions =>
        _permissions.AsReadOnly();

    public static Role Create(
        RoleName name,
        string title,
        IEnumerable<Permission> permissions)
    {
        var role = new Role(RoleId.CreateUniqueId())
        {
            Name = name,
            Title = title
        };

        role.SetPermissions(permissions);

        return role;
    }
    public void UpdateTitle(string title)
    {
        SetTitle(title);
    }

    public void SetPermissions(IEnumerable<Permission> permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);

        var permissionList = permissions.Distinct().ToList();

        foreach (var permission in permissionList)
        {
            if (!Roles.Permissions.All.Any(p => p.Equals(permission)))
                throw new InvalidOperationException($"Permission '{permission}' is not registered.");
        }

        _permissions.Clear();
        _permissions.AddRange(permissionList);
    }

    public void GrantPermission(Permission permission)
    {
        if (_permissions.Contains(permission))
            return;

        _permissions.Add(permission);
    }

    public void RevokePermission(Permission permission)
    {
        _permissions.Remove(permission);
    }

    public bool HasPermission(Permission permission)
    {
        return _permissions.Contains(permission);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Role title is required.");

        Title = title.Trim();
    }
}
