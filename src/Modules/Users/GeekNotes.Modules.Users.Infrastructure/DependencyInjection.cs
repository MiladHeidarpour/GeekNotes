using BuildingBlocks.User.Abstractions;
using GeekNotes.Modules.Users.Infrastructure.Persistence.Seed;
using GeekNotes.Modules.Users.Infrastructure.Persistence.Services;

namespace GeekNotes.Modules.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(UserDbContextSchema.DefaultConnectionStringName));
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
public static class UserInfrastructureExtensions
{
    public static async Task InitialiseUserModuleAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserContext>();

        await context.Database.MigrateAsync();
        await UserSeeder.SeedAsync(context);
    }
}