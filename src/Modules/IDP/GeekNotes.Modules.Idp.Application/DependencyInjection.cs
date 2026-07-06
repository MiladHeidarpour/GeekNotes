using Microsoft.Extensions.DependencyInjection;

namespace GeekNotes.Modules.Idp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdpApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            var application = typeof(IAssemblyMarker);
            configure.RegisterServicesFromAssembly(application.Assembly);
        });

        return services;
    }
}
