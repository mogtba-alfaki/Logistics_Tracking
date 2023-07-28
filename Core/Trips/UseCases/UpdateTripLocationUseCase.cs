using Core.Enums;
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
            throw new Exception("Trip Not Started Yet"); 
        } 
        
        // TODO should check if the trip is on a restricted area

        var tripLocation = new TripLocation {
            Id = IdGenerator.Generate(),
            Altitude = dto.Altitude,
            Heading = dto.Heading,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Speed = dto.Speed,
            TimeSpent = 0, //   TODO: CALCULATE LATER BASED ON THE PREVIOUS LOCATION,
            TripId = dto.TripId,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        
        await _tripLocationsRepository.AddTripLocation(tripLocation);
        return true;
    }
}