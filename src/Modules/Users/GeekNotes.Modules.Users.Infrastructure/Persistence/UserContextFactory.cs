using Microsoft.EntityFrameworkCore.Design;

namespace GeekNotes.Modules.Users.Infrastructure.Persistence;

public sealed class UserContextFactory
    : IDesignTimeDbContextFactory<UserContext>
{
    public UserContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<UserContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,14331;Database=GeekNotesDbContext;User Id=sa;Password=sqlserverExpress@test.;TrustServerCertificate=True;Encrypt=False;");

        return new UserContext(optionsBuilder.Options);
    }
}
