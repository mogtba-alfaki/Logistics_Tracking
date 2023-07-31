using Core.Geofencing;
using Core.RestrictedAreas;
using Core.RestrictedAreas.UseCases;
using Core.Trips;
using Core.Trips.UseCases;
using Core.Trucks;
using Core.Trucks.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Core; 

public static class CoreLayerServicesExtension {
    public static IServiceCollection AddCoreDependencies(this IServiceCollection services) {
        services.AddScoped<TruckUseCases, TruckUseCases>();
        services.AddScoped<TrucksService, TrucksService>();
        
        services.AddScoped<RestrictedAreasUseCases, RestrictedAreasUseCases>();
        services.AddScoped<RestrictedAreasService, RestrictedAreasService>();
        
        services.AddScoped<AddTripUseCase, AddTripUseCase>();
        services.AddScoped<ListTripsUseCase, ListTripsUseCase>();
        services.AddScoped<EndTripUseCase, EndTripUseCase>(); 
        services.AddScoped<UpdateTripLocationUseCase, UpdateTripLocationUseCase>();
        
        services.AddScoped<TripService, TripService>();
        services.AddScoped<MapHelper, MapHelper>(); 
        
        return services;
    }
}