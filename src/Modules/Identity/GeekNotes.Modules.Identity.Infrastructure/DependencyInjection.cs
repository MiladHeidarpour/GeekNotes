using GeekNotes.Modules.Identity.Infrastructure.Persistence;
using GeekNotes.Modules.Identity.Infrastructure.Persistence.Seed;

namespace GeekNotes.Modules.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(IdentityDbContextSchema.DefaultConnectionStringName));
        });

        services.AddScoped<IRoleRepository, RoleRepository>();
        return services;
    }
}
public static class IdentityInfrastructureExtensions
{
    public static async Task InitialiseIdentityModuleAsync( this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();

        await context.Database.MigrateAsync();
        await RoleSeeder.SeedAsync(context);
    }
}