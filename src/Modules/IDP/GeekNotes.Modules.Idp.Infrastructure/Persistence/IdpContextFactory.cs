using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence;

internal class IdpContextFactory : IDesignTimeDbContextFactory<IdpDbContext>
{
    public IdpDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<IdpDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,14331;Database=GeekNotesDbContext;User Id=sa;Password=sqlserverExpress@test.;TrustServerCertificate=True;Encrypt=False;");

        return new IdpDbContext(optionsBuilder.Options);
    }
}
