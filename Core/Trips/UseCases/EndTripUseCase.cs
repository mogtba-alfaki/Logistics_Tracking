using Core.Enums;
using Core.Exceptions;
using Core.Geofencing;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Core.Trips.UseCases; 

public class EndTripUseCase {
    private readonly MapHelper _mapHelper;
    private readonly TripRepository _repository;
    private readonly TripLocationsRepository _locationsRepository;
    private readonly TrackingGenericRepository _trackingGenericRepository;

    public EndTripUseCase(MapHelper mapHelper, TripRepository repository, TripLocationsRepository locationsRepository, TrackingGenericRepository trackingGenericRepository) {
        _mapHelper = mapHelper;
        _repository = repository;
        _locationsRepository = locationsRepository;
        _trackingGenericRepository = trackingGenericRepository;
    }

    public async Task<Trip>  End(string tripId) {
        var trip = await _repository.GetTripById(tripId);
        
        if (trip.Status != (int) TripStatuses.STARTED) {
            throw new UnCorrectTripStatusException("Trip cannot be ended at this current status"); 
        }

        var tripLocations = await _locationsRepository
            .GetTripLocationsByTripId(tripId);

        var startPoint = $"{tripLocations[0].Latitude},{tripLocations[0].Longitude}"; 
        var endPoint = $"{tripLocations[^1].Latitude},{tripLocations[^1].Longitude}";

        var fullTripRoute = await _mapHelper.GetFullRoute(startPoint, endPoint);
        trip.FullRoute = fullTripRoute;
        trip.Status = (int) TripStatuses.ENDED;

        var truck = await _trackingGenericRepository.GetTruckById(trip.TruckId); 
        await _trackingGenericRepository
            .ChangeTruckStatus(truck.Id,(int) TruckStatuses.IDLE);
        return await _repository.UpdateTrip(trip);
    }
}