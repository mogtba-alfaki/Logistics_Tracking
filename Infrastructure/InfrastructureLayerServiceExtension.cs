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
}