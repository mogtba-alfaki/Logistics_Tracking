using AutoMapper;
using Core.Geofencing;
using Core.RestrictedAreas;
using Core.RestrictedAreas.Dto;
using Core.RestrictedAreas.UseCases;
using Core.Trips;
using Core.Trips.UseCases;
using Core.Trucks;
using Core.Trucks.UseCases;
using Core.Users;
using Core.Users.UseCases;
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
        
        services.AddScoped<UsersUseCases, UsersUseCases>();
        services.AddScoped<UsersService, UsersService>();
        
        services.AddScoped<SpatialDataUtility, SpatialDataUtility>();

        services.AddScoped<RestrictedAreaMapper, RestrictedAreaMapper>();
        services.AddScoped<TripsMapper, TripsMapper>();
        services.AddScoped<TrucksMapper, TrucksMapper>();
        services.AddScoped<UsersMapper, UsersMapper>(); 
        
        return services;
    }
}