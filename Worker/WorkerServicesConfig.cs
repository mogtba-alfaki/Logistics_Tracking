using Core.Geofencing;
using Core.Interfaces;
using Core.Repositories;
using Core.Trips.UseCases;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.GeoApisProviders;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker.MessageBroker;

namespace Worker; 

public static class WorkerServicesConfig {
    public static IHostBuilder ConfigureWorkerServices(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            services.AddScoped<UpdateTripLocationUseCase, UpdateTripLocationUseCase>();
            services.AddDbContext<TrackingContext>();
            services.AddScoped<ITripLocationRepository, TripLocationsRepository>(); 
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IRestrictedAreaRepository, RestrictedAreaRepository>(); 
            services.AddScoped<ISpatialDataServices, SpatialDataServices>(); 
            services.AddScoped<ILogger, Logger>();
        }); 
        return hostBuilder; 
    }
    
    public static IHostBuilder LoadEnvVariables(this IHostBuilder hostBuilder) {
        hostBuilder.ConfigureServices(services => {
            foreach (string line in File.ReadAllLines("./.env")) {
                var parts = line.Split("=");
                if (parts.Length != 2)
                    continue;
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        });
        return hostBuilder; 
    }
}