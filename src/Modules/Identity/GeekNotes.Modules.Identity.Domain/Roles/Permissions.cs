namespace GeekNotes.Modules.Identity.Domain.Roles;

public static class Permissions
{
    public static readonly Permission CreateArticle =
        Permission.Create("CREATE_ARTICLE");

    public static readonly Permission UpdateArticle =
        Permission.Create("UPDATE_ARTICLE");

    public static readonly Permission DeleteArticle =
        Permission.Create("DELETE_ARTICLE");

    public static readonly Permission PublishArticle =
        Permission.Create("PUBLISH_ARTICLE");

    public static readonly Permission ManageUsers =
        Permission.Create("MANAGE_USERS");

    public static readonly Permission ManageRoles =
        Permission.Create("MANAGE_ROLES");

    public static readonly Permission User =
        Permission.Create("USER");


    public static IReadOnlyList<Permission> All =>
    [
        CreateArticle,
        UpdateArticle,
        DeleteArticle,
        PublishArticle,
        ManageUsers,
        ManageRoles,
        User
    ];
}