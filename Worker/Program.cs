using Core.Trips.Dto;
using Core.Trips.UseCases;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Worker;
using Worker.Services;

var builder = new HostBuilder(); 

builder.ConfigureWorkerServices();
builder.LoadEnvVariables(); 

var host = builder.Build();

var updateTripLocationUseCase = host.Services.GetRequiredService<UpdateTripLocationUseCase>();
var locationServices = new LocationServices(updateTripLocationUseCase); 

var dbContext = host.Services.GetRequiredService<TrackingContext>(); 
var worker = new RabbitMqWorker(dbContext, locationServices);
worker.Run(); 
