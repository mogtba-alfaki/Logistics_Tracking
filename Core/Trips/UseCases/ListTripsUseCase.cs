using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.Trips.UseCases; 

public class ListTripsUseCase {
    private readonly TripRepository _repository;

    public ListTripsUseCase(TripRepository repository) {
        _repository = repository;
    }

    public async Task<List<Trip>> ListTrips(CustomQueryParameters customQueryParameters) {
        return await _repository.ListTrips(customQueryParameters); 
    }
}