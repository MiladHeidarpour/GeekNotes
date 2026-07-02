namespace GeekNotes.Modules.Identity.Infrastructure.Persistence;

public static class IdentityDbContextSchema
{
    public const string DefaultSchema = "IDPModule";
    public const string DefaultConnectionStringName = "SvcDbContext";

    public static class RoleDbSchema
    {
        public const string TableName = "Roles";
        public const string ForeignKey = "RoleId";
        public const string Permissions = "RolePermissions";
    }
}
