using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure; 

public static class InfrastructureLayerServiceExtension {
    
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) {
        services.AddScoped<TruckRepository, TruckRepository>();
        services.AddScoped<RestrictedAreaRepository, RestrictedAreaRepository>();
        services.AddScoped<TrackingGenericRepository, TrackingGenericRepository>(); 
        services.AddScoped<TripLocationsRepository, TripLocationsRepository>();
        services.AddScoped<TripRepository, TripRepository>(); 
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
}