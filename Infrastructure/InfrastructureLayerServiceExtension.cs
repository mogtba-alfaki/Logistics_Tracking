using System.Text;
using Core.Geofencing;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure; 

public static class InfrastructureLayerServiceExtension {
    
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) {
        services.AddScoped<RestrictedAreaRepository, RestrictedAreaRepository>();
        services.AddScoped<TrackingGenericRepository, TrackingGenericRepository>(); 
        services.AddScoped<TripLocationsRepository, TripLocationsRepository>();
        services.AddScoped<TripRepository, TripRepository>();
        services.AddScoped<IMapProvider, GraphHopper>();
        services.AddScoped<UserRepository, UserRepository>();

        services.AddScoped<MultiPartFileHandler, MultiPartFileHandler>();
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