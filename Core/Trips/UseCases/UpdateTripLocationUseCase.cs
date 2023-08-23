using Core.Enums;
using Core.Exceptions;
using Core.Geofencing;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.Trips.Dto;
using Domain.Entities;

namespace Core.Trips.UseCases; 

public class UpdateTripLocationUseCase {
    private readonly ITripLocationRepository _tripLocationsRepository;
    private readonly ITripRepository _tripRepository;
    private readonly IRestrictedAreaRepository _restrictedAreaRepository; 
    private readonly ISpatialDataServices _spatialDataServices;
    private readonly ILogger _logger;

    public UpdateTripLocationUseCase(ITripLocationRepository tripLocationsRepository, ITripRepository tripRepository, IRestrictedAreaRepository restrictedAreaRepository, ISpatialDataServices spatialDataServices, ILogger logger) {
        _tripLocationsRepository = tripLocationsRepository;
        _tripRepository = tripRepository;
        _restrictedAreaRepository = restrictedAreaRepository;
        _spatialDataServices = spatialDataServices;
        _logger = logger;
    }

    public async Task<bool> Update(TripLocationDto dto) {
        _logger.LogInfo($"UpdateTripLocationUseCase, Location: {dto}");
        var trip = await _tripRepository.GetById(dto.TripId);        
        
        if (trip.Status != (int) TripStatuses.STARTED) {
            throw new UnCorrectTripStatusException("Trip Not Started"); 
        } 
        
        var tripRestrictedAreas = await _restrictedAreaRepository
            .GetByTripId(trip.Id);
        
        foreach (var area in tripRestrictedAreas) {
            try {
                var areaViolated  = await _spatialDataServices.IsLocationWithinPolygon(area.AreaPolygon, 
                new LocationCoordinate(dto.Latitude, dto.Longitude));
                if (areaViolated) {
                    area.Violated = true; 
                    await _restrictedAreaRepository.Update(area); 
                    throw new RestrictedAreaInPathException("Area Violated"); 
                }
            }
            catch (Exception exception) {
                throw new SpatialDataApisException(exception.Message); 
            }
        }
        
        var previousTripLocation = await _tripLocationsRepository.GetLatestTripLocation(trip.Id);
        
        var tripLocation = new TripLocation {
            Id = IdGenerator.Generate(),
            Altitude = dto.Altitude,
            Heading = dto.Heading,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Speed = dto.Speed,
            TimeSpent = 0,
            TripId = dto.TripId,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        
        tripLocation.TimeSpent = CalculateTimeSpentAtLocation(tripLocation,
            previousTripLocation); 
        
        await _tripLocationsRepository.Create(tripLocation);
        return true;
    }


    private int CalculateTimeSpentAtLocation(TripLocation current, TripLocation previous) {
        var timeSpent = 
            current.CreatedAt
            .Subtract(previous.CreatedAt);
        var days = timeSpent.Days;
        var hours = timeSpent.Hours;
        var seconds = timeSpent.Seconds;
        var timeSpentInSeconds = (days * 24 * 60 * 60) + (hours * 60 * 60) + seconds;
        return timeSpentInSeconds;
    }
}