using System.Text.Json.Serialization;
using Api.Helpers;
using Core;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.LoadEnvironmentVariables();
builder.Services.ConfigureDbContext();
builder.Services.AddCoreDependencies(); 
builder.Services.AddInfrastructureDependencies();
builder.Services.ConfigureJwtAuthentication();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.HandelGlobalExceptions();
app.MapControllers();

app.Run();