namespace GeekNotes.Modules.Users.Domain;

public sealed class UserRole
{
    private UserRole()
    {
    }

    public Guid RoleId { get; private set; }

    private UserRole(Guid roleId)
    {
        RoleId = roleId;
    }

    public static UserRole Create(Guid roleId)
        => new(roleId);
}
