using Microsoft.Extensions.DependencyInjection;

namespace GeekNotes.Modules.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            var application = typeof(IAssemblyMarker);
            configure.RegisterServicesFromAssembly(application.Assembly);
        });

        return services;
    }
}
