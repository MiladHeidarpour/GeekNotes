using GeekNotes.Modules.Identity.Infrastructure;
using GeekNotes.Modules.Users.Infrastructure;
using GeekNotes.Modules.Users.Presentation;
using GeekNotes.Modules.Users.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddUsersApplication();
builder.Services.AddUsersInfrastructure(builder.Configuration);
builder.Services.AddUsersPresentation();

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddSwaggerGen();
var app = builder.Build();

await app.Services.InitialiseIdentityModuleAsync();
await app.Services.InitialiseUserModuleAsync();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
