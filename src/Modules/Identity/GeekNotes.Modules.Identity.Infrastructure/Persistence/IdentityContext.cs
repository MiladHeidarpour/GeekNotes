namespace GeekNotes.Modules.Identity.Infrastructure.Persistence;

public class IdentityContext : ModuleDbContext<IdentityContext>
{
    protected override string Schema => IdentityDbContextSchema.DefaultSchema;

    public DbSet<Role> Roles => Set<Role>();

    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }
}