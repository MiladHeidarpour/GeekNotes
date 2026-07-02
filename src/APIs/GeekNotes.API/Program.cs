using GeekNotes.Modules.Identity.Infrastructure;
using GeekNotes.Modules.Users.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddUserModule(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
var app = builder.Build();

await app.Services.InitialiseIdentityModuleAsync();
await app.Services.InitialiseUserModuleAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
