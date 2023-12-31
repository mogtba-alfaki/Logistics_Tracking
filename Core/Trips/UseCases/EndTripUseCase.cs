using Core.Enums;
using Core.Exceptions;
using Core.Geofencing;
using Core.Interfaces;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Core.Trips.Dto;

namespace Core.Trips.UseCases; 

public class EndTripUseCase {
    private readonly SpatialDataUtility _spatialDataUtility;
    private readonly ITripRepository _repository;
    private readonly ITripLocationRepository _locationsRepository;
    private readonly ITruckRepository _truckRepository;
    private readonly ILogger _logger;
    private readonly TripsMapper _mapper; 

    public EndTripUseCase(SpatialDataUtility mapHelper, ITripRepository repository, ITripLocationRepository locationsRepository, ITruckRepository truckRepository, ILogger logger) {
        _spatialDataUtility = mapHelper;
        _repository = repository;
        _locationsRepository = locationsRepository;
        _truckRepository = truckRepository;
        _logger = logger;
    }

    public async Task<GetTripDto>  End(string tripId) {
        _logger.LogInfo($"EndTripUseCase, TripId: {tripId}");
        var trip = await _repository.GetById(tripId);
        
        if (trip.Status != (int) TripStatuses.STARTED) {
            throw new UnCorrectTripStatusException("Trip cannot be ended at this current status"); 
        }

        var tripLocations = await _locationsRepository
            .GetTripLocationsByTripId(tripId);

        var startPoint = $"{tripLocations[0].Latitude},{tripLocations[0].Longitude}"; 
        var endPoint = $"{tripLocations[^1].Latitude},{tripLocations[^1].Longitude}";

        var fullTripRoute = await _spatialDataUtility.GetEncodedRoute(startPoint, endPoint);
        trip.FullRoute = fullTripRoute;
        trip.Status = (int) TripStatuses.ENDED;

        var truck = await _truckRepository.GetById(trip.TruckId); 
        await _truckRepository
            .ChangeTruckStatus(truck.Id,(int) TruckStatuses.IDLE);
        var endedTrip = await _repository.Update(trip);
        return _mapper.MapToDto(endedTrip); 
    }
}