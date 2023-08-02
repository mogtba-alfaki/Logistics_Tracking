using Core.Enums;
using Core.Exceptions;
using Core.Trips.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.Trips.UseCases; 

public class UpdateTripLocationUseCase {
    private readonly TripLocationsRepository _tripLocationsRepository;
    private readonly TripRepository _tripRepository;

    public UpdateTripLocationUseCase(TripLocationsRepository tripLocationsRepository, TripRepository tripRepository) {
        _tripLocationsRepository = tripLocationsRepository;
        _tripRepository = tripRepository;
    }

    public async Task<bool> Update(TripLocationDto dto) {
        Console.WriteLine(dto);
        var trip = await _tripRepository.GetTripById(dto.TripId);
        
        
        if (trip.Status != (int) TripStatuses.STARTED) {
            throw new UnCorrectTripStatusException("Trip Not Started Yet"); 
        } 
        
        // TODO should check if the trip is on a restricted area 
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
        
        await _tripLocationsRepository.AddTripLocation(tripLocation);
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