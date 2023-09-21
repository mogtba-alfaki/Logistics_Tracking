using System.Text.Json;
using Core.Helpers;
using Core.Interfaces.MessageQueue;
using Core.Trips.Dto;
using Core.Trips.UseCases;

namespace Core.Trips; 

public class TripService {
    private readonly AddTripUseCase AddTripUseCase;
    private readonly ListTripsUseCase ListTripsUseCase;
    private readonly EndTripUseCase EndTripUseCase;
    private readonly IPublisher _publisher;

    public TripService(AddTripUseCase addTripUseCase, ListTripsUseCase listTripsUseCase, EndTripUseCase endTripUseCase, IPublisher publisher) {
        AddTripUseCase = addTripUseCase;
        ListTripsUseCase = listTripsUseCase;
        EndTripUseCase = endTripUseCase;
        _publisher = publisher;
    }

    public async Task<List<GetTripDto>> ListTrips(CustomQueryParameters customQueryParameters) {
        return await ListTripsUseCase.ListTrips(customQueryParameters); 
    }
    public async Task<GetTripDto> AddTrip(AddTripDto dto) {
        return await AddTripUseCase.AddTrip(dto); 
    }

    public async Task<bool> UpdateTripLocation(TripLocationDto dto) {
        var messageString = JsonSerializer.Serialize(dto);
        await _publisher.PublishMessage(messageString);
        return true; 
    }
    
    public async Task<GetTripDto> EndTrip(string tripId) {
        return await EndTripUseCase.End(tripId); 
    }
}