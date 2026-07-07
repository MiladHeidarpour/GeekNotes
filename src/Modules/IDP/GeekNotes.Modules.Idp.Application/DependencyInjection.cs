namespace GeekNotes.Modules.Idp.Application;

public static class DependencyInjection
{
    private readonly static Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
    public static IServiceCollection AddIdpApplication(this IServiceCollection services)
    {
        services.AddMediatR(configure =>
        {
            var application = typeof(IAssemblyMarker);
            configure.RegisterServicesFromAssembly(application.Assembly);
        });

        services.AddValidatorsFromAssemblies(Assemblies);
        return services;
    }
}
