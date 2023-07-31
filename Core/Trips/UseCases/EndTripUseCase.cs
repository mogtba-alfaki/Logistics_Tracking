using Core.Enums;
using Core.Geofencing;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Core.Trips.UseCases; 

public class EndTripUseCase {
    private readonly MapHelper _mapHelper;
    private readonly TripRepository _repository;
    private readonly TripLocationsRepository _locationsRepository;

    public EndTripUseCase(MapHelper mapHelper, TripRepository repository, TripLocationsRepository locationsRepository) {
        _mapHelper = mapHelper;
        _repository = repository;
        _locationsRepository = locationsRepository;
    }

    public async Task<Trip>  End(string tripId) {
        var trip = await _repository.GetTripById(tripId);
        
        if (trip.Status != (int) TripStatuses.STARTED) {
            throw new Exception("Trip Cannot Be Ended"); 
        }

        var tripLocations = await _locationsRepository
            .GetTripLocationsByTripId(tripId);

        var startPoint = $"{tripLocations[0].Latitude},{tripLocations[0].Longitude}"; 
        var endPoint = $"{tripLocations[^1].Latitude},{tripLocations[^1].Longitude}";

        var fullTripRoute = await _mapHelper.GetFullRoute(startPoint, endPoint);
        trip.FullRoute = fullTripRoute;
        trip.Status = (int) TripStatuses.ENDED; 
        
        return await _repository.UpdateTrip(trip); 
        
    }
}