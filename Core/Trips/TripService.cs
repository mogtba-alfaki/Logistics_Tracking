using Core.Helpers;
using Core.Trips.Dto;
using Core.Trips.UseCases;
using Domain.Entities;

namespace Core.Trips; 

public class TripService {
    private readonly AddTripUseCase AddTripUseCase;
    private readonly ListTripsUseCase ListTripsUseCase;
    private readonly UpdateTripLocationUseCase UpdateTripLocationUseCase;
    private readonly EndTripUseCase EndTripUseCase;
    public TripService(AddTripUseCase addTripUseCase, ListTripsUseCase listTripsUseCase, UpdateTripLocationUseCase updateTripLocationUseCase, EndTripUseCase endTripUseCase) {
        AddTripUseCase = addTripUseCase;
        ListTripsUseCase = listTripsUseCase;
        UpdateTripLocationUseCase = updateTripLocationUseCase;
        EndTripUseCase = endTripUseCase;
    }

    public async Task<List<GetTripDto>> ListTrips(CustomQueryParameters customQueryParameters) {
        return await ListTripsUseCase.ListTrips(customQueryParameters); 
    }
    public async Task<GetTripDto> AddTrip(AddTripDto dto) {
        return await AddTripUseCase.AddTrip(dto); 
    }

    public async Task<bool> UpdateTripLocation(TripLocationDto dto) {
        return await UpdateTripLocationUseCase.Update(dto); 
    }
    
    public async Task<GetTripDto> EndTrip(string tripId) {
        return await EndTripUseCase.End(tripId); 
    }
}