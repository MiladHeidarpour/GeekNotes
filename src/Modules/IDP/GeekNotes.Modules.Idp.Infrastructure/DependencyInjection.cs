using GeekNotes.Modules.Idp.Domain;
using GeekNotes.Modules.Idp.Domain.Credentials;
using GeekNotes.Modules.Idp.Domain.ExternalLogins;
using GeekNotes.Modules.Idp.Domain.Sessions;
using GeekNotes.Modules.Idp.Domain.Verifications;
using GeekNotes.Modules.Idp.Infrastructure.Persistence;
using GeekNotes.Modules.Idp.Infrastructure.Persistence.Repositories;
using GeekNotes.Modules.Idp.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekNotes.Modules.Idp.Infrastructure;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddIdpInfrastructure(
//        this IServiceCollection services,
//        IConfiguration configuration)
//    {
//        services.Configure<JwtSettings>(
//            configuration.GetSection(JwtSettings.SectionName));

//        services.AddSingleton<JwtSecurityKeyProvider>();

//        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
//        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

//        return services;
//    }
//}

public static class DependencyInjection
{
    public static IServiceCollection AddIdpInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdpDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(IdpDbContextSchema.DefaultConnectionStringName));
        });

        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<JwtSecurityKeyProvider>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        services.AddScoped<ICredentialRepository, CredentialRepository>();
        services.AddScoped<IExternalLoginRepository, ExternalLoginRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IVerificationRepository, VerificationRepository>();

        services.AddScoped<IPasswordHasher, IdentityPasswordHasher>();
        return services;
    }
}