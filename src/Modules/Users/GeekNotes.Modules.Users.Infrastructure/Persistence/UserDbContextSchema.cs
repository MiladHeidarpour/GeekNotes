namespace GeekNotes.Modules.Users.Infrastructure.Persistence;

public static class UserDbContextSchema
{
    public const string DefaultSchema = "UserModule";
    public const string DefaultConnectionStringName = "SvcDbContext";

    public const string TableName = "Users";
    public const string ForeignKey = "UserId";
    public const string UserRoles = "UserRoles";
}
