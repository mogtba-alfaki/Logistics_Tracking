using System.Text.Json.Serialization;
using Core;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); 

builder.Services.ConfigureDbContext();
builder.Services.AddCoreDependencies(); 
builder.Services.AddInfrastructureDependencies();
builder.Services.LoadEnvironmentVariables();

var app = builder.Build();

app.MapControllers(); 

app.Run();