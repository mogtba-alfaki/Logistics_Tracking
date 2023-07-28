using Core.Trips.Dto;
using Core.Trips.UseCases;
using Domain.Entities;
using Infrastructure.Util;

namespace Core.Trips; 

public class TripService {
    private readonly AddTripUseCase AddTripUseCase;
    private readonly ListTripsUseCase ListTripsUseCase;
    private readonly UpdateTripLocationUseCase UpdateTripLocationUseCase; 
    
    public TripService(AddTripUseCase addTripUseCase, ListTripsUseCase listTripsUseCase, UpdateTripLocationUseCase updateTripLocationUseCase) {
        AddTripUseCase = addTripUseCase;
        ListTripsUseCase = listTripsUseCase;
        UpdateTripLocationUseCase = updateTripLocationUseCase;
    }

    public async Task<List<Trip>> ListTrips(CustomQueryParameters customQueryParameters) {
        return await ListTripsUseCase.ListTrips(customQueryParameters); 
    }
    public async Task<Trip> AddTrip(AddTripDto dto) {
        return await AddTripUseCase.AddTrip(dto); 
    }

    public async Task<bool> UpdateTripLocation(TripLocationDto dto) {
        return await UpdateTripLocationUseCase.Update(dto); 
    }
}