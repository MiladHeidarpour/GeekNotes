using GeekNotes.BuildingBlocks.Infrastructure;
using GeekNotes.Modules.Idp.Domain.Credentials;
using GeekNotes.Modules.Idp.Domain.ExternalLogins;
using GeekNotes.Modules.Idp.Domain.Sessions;
using GeekNotes.Modules.Idp.Domain.Verifications;
using Microsoft.EntityFrameworkCore;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence;


public class IdpDbContext : ModuleDbContext<IdpDbContext>
{
    protected override string Schema => IdpDbContextSchema.DefaultSchema;

    public DbSet<Credential> Credentials => Set<Credential>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Verification> Verifications => Set<Verification>();
    public DbSet<ExternalLogin> ExternalLogins => Set<ExternalLogin>();

    public IdpDbContext(DbContextOptions<IdpDbContext> options)
        : base(options)
    {
    }
}