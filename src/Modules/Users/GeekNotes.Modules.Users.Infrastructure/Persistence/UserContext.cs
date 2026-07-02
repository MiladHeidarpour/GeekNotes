namespace GeekNotes.Modules.Users.Infrastructure.Persistence;

public class UserContext : ModuleDbContext<UserContext>
{
    protected override string Schema => UserDbContextSchema.DefaultSchema;

    public DbSet<User> Users => Set<User>();

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }
}