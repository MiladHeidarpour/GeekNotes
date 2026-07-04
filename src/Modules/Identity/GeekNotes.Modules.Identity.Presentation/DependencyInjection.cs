using FluentValidation;
using Mapster;
using MapsterMapper;
using System.Reflection;
using System.Reflection.Metadata;

namespace GeekNotes.Modules.Identity.Presentation;

public static class DependencyInjection
{
    private readonly static Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
    public static IServiceCollection AddIdentityPresentation(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(AssemblyReference).Assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddControllers().AddApplicationPart(typeof(AssemblyReference).Assembly);

        services.AddValidatorsFromAssemblies(Assemblies);
        return services;
    }
}
