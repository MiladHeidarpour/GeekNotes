using GeekNotes.BuildingBlocks.Domain;

namespace GeekNotes.Modules.Users.Domain;
public sealed class User : AggregateRoot<UserId>
{
    private readonly List<UserRole> _roles = [];

    private User(UserId id) : base(id)
    {
    }

    private User() : base(UserId.CreateUnique())
    {
    }
    public Email Email { get; private set; } = null!;

    public PhoneNumber PhoneNumber { get; private set; } = null!;

    public UserName UserName { get; private set; } = null!;

    public string FullName { get; private set; } = null!;

    public string? Avatar { get; private set; }

    public string PasswordHash { get; private set; } = null!;

    public DateTime JoinedOnUtc { get; private set; }

    public string? GithubId { get; private set; }

    public void LinkGithubAccount(string githubId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(githubId);

        GithubId = githubId;
    }

    public IReadOnlyCollection<UserRole> Roles
        => _roles.AsReadOnly();

    public static User Create(
        Email email,
        PhoneNumber phoneNumber,
        UserName userName,
        string fullName,
        string passwordHash,
        IEnumerable<Guid> roleIds)
    {
        var user = new User(UserId.CreateUnique())
        {
            Email = email,
            PhoneNumber = phoneNumber,
            UserName = userName,
            FullName = fullName,
            PasswordHash = passwordHash,
            JoinedOnUtc = DateTime.UtcNow
        };

        foreach (var roleId in roleIds.Distinct())
        {
            user._roles.Add(UserRole.Create(roleId));
        }

        return user;
    }

    public void ChangeAvatar(string avatar)
    {
        Avatar = avatar;
    }

    public void ChangeFullName(string fullName)
    {
        FullName = fullName;
    }

    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void AssignRole(Guid roleId)
    {
        if (_roles.Any(x => x.RoleId == roleId))
            return;

        _roles.Add(UserRole.Create(roleId));
    }

    public void RemoveRole(Guid roleId)
    {
        _roles.RemoveAll(x => x.RoleId == roleId);
    }

    public void Update(string fullName, Email email, PhoneNumber phoneNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static User Register(Email email, PhoneNumber phoneNumber, UserName userName, string fullName, string password, Guid roleId)
    {
        var user = new User(UserId.CreateUnique())
        {
            Email = email,
            PhoneNumber = phoneNumber,
            UserName = userName,
            FullName = fullName,
            PasswordHash = password,
            JoinedOnUtc = DateTime.UtcNow
        };

        user._roles.Add(UserRole.Create(roleId));
        return user;
    }
}
