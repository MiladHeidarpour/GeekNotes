using Microsoft.EntityFrameworkCore.Design;

namespace GeekNotes.Modules.Identity.Infrastructure.Persistence;

public sealed class IdentityContextFactory
    : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<IdentityContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,14331;Database=GeekNotesDbContext;User Id=sa;Password=sqlserverExpress@test.;TrustServerCertificate=True;Encrypt=False;");

        return new IdentityContext(optionsBuilder.Options);
    }
}
