using Microsoft.EntityFrameworkCore;

namespace GeekNotes.BuildingBlocks.Infrastructure;

public abstract class ModuleDbContext<TContext> : DbContext
    where TContext : DbContext
{
    protected abstract string Schema { get; }

    protected ModuleDbContext(DbContextOptions<TContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
            modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
