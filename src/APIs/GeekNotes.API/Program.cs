using GeekNotes.Modules.Identity.Application;
using GeekNotes.Modules.Identity.Infrastructure;
using GeekNotes.Modules.Identity.Presentation;
using GeekNotes.Modules.Idp.Application;
using GeekNotes.Modules.Idp.Infrastructure;
using GeekNotes.Modules.Idp.Presentation;
using GeekNotes.Modules.Users.Application;
using GeekNotes.Modules.Users.Infrastructure;
using GeekNotes.Modules.Users.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUsersApplication();
builder.Services.AddUsersInfrastructure(builder.Configuration);
builder.Services.AddUsersPresentation();

builder.Services.AddIdentityApplication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddIdentityPresentation();

builder.Services.AddIdpApplication();
builder.Services.AddIdpInfrastructure(builder.Configuration);
builder.Services.AddIdpPresentation();

builder.Services.AddControllers();
builder.Services.AddOpenApi();


//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc(
//        "v1",
//        new OpenApiInfo
//        {
//            Title = "GeekNotes API",
//            Version = "v1"
//        });

//    options.AddSecurityDefinition(
//        "Bearer",
//        new OpenApiSecurityScheme
//        {
//            Name = "Authorization",
//            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//            In = ParameterLocation.Header,
//            Type = SecuritySchemeType.Http,
//            Scheme = "bearer",
//            BearerFormat = "JWT"
//        });

//    options.AddSecurityRequirement(
//        new OpenApiSecurityRequirement
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference
//                    {
//                        Type =re
//                        Id = "Bearer"
//                    }
//                },
//                Array.Empty<string>()
//            }
//        });
//});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var sp = builder.Services.BuildServiceProvider();
        var keyProvider = sp.GetRequiredService<JwtSecurityKeyProvider>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = keyProvider.GetKey()
        };
    });
var app = builder.Build();

await app.Services.InitialiseIdentityModuleAsync();
await app.Services.InitialiseUserModuleAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
