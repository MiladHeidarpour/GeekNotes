using Microsoft.Extensions.DependencyInjection;

namespace GeekNotes.Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            var application = typeof(IAssemblyMarker);
            configure.RegisterServicesFromAssembly(application.Assembly);
        });

        return services;
    }
}
