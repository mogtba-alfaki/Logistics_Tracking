using Core.Enums;
using Core.Exceptions;
using Core.Geofencing;
using Core.Repositories;
using Domain.Entities;

namespace Core.Trips.UseCases; 

public class EndTripUseCase {
    private readonly MapHelper _mapHelper;
    private readonly ITripRepository _repository;
    private readonly ITripLocationRepository _locationsRepository;
    private readonly ITruckRepository _truckRepository;

    public EndTripUseCase(MapHelper mapHelper, ITripRepository repository, ITripLocationRepository locationsRepository, ITruckRepository truckRepository) {
        _mapHelper = mapHelper;
        _repository = repository;
        _locationsRepository = locationsRepository;
        _truckRepository = truckRepository;
    }

    public async Task<Trip>  End(string tripId) {
        var trip = await _repository.GetById(tripId);
        
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

        var truck = await _truckRepository.GetById(trip.TruckId); 
        await _truckRepository
            .ChangeTruckStatus(truck.Id,(int) TruckStatuses.IDLE);
        return await _repository.Update(trip);
    }
}