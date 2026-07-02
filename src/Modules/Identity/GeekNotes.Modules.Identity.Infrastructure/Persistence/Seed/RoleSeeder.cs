namespace GeekNotes.Modules.Identity.Infrastructure.Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(IdentityContext dbContext)
    {
        if (await dbContext.Roles.AnyAsync())
            return;

        var roles = new List<Role>
        {
            Role.Create(
                RoleName.Create("ADMIN"),
                "System Administrator",
                [
                    Permissions.ManageUsers,
                    Permissions.ManageRoles,
                    Permissions.CreateArticle,
                    Permissions.UpdateArticle,
                    Permissions.DeleteArticle,
                    Permissions.PublishArticle,
                ]),

            Role.Create(
                RoleName.Create("WRITER"),
                "Content Writer",
                [
                    Permissions.CreateArticle,
                    Permissions.UpdateArticle
                ]),

            Role.Create(
                RoleName.Create("EDITOR"),
                "Content Editor",
                [
                    Permissions.PublishArticle,
                    Permissions.UpdateArticle
                ]),

            Role.Create(
                RoleName.Create("USER"),
                "Simple User",
                [
                    Permissions.User,
                ])
        };

        await dbContext.Roles.AddRangeAsync(roles);
        await dbContext.SaveChangesAsync();
    }
}
