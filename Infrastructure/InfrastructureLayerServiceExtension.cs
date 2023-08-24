using System.Text;
using Core.Geofencing;
using Core.Interfaces;
using Core.Repositories;
using Infrastructure.Database;
using Infrastructure.GeoApisProviders;
using Infrastructure.Helpers.AwsS3;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure; 

public static class InfrastructureLayerServiceExtension {
    
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) {
        services.AddScoped<IRestrictedAreaRepository, RestrictedAreaRepository>();
        services.AddScoped<ITripLocationRepository, TripLocationsRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>(); 
        services.AddScoped<ITruckRepository, TruckRepository>();
        
        services.AddScoped<ISpatialDataServices, SpatialDataServices>();
        services.AddScoped<IObjectStorageProvider, AwsS3Helper>();
        services.AddScoped<ILogger, Logger>(); 
        
        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services) {
        services.AddDbContext<TrackingContext>();
        return services; 
    }

    public static void LoadEnvironmentVariables(this IServiceCollection services) {
        // TODO MAKE THE PATH DYNAMIC 
        foreach(string line in File.ReadAllLines("../Infrastructure/.env")) {
            var parts = line.Split("=");
            if (parts.Length != 2)
                continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
    
    public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services) {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
                var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
                var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
                
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true, 
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            }); 
        return services;
    }
}