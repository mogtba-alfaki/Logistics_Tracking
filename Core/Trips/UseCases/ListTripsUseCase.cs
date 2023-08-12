using Core.Helpers;
using Core.Repositories;
using Domain.Entities;

namespace Core.Trips.UseCases; 

public class ListTripsUseCase {
    private readonly ITripRepository _repository;

    public ListTripsUseCase(ITripRepository repository) {
        _repository = repository;
    }

    public async Task<List<Trip>> ListTrips(CustomQueryParameters customQueryParameters) {
        return await _repository.List(customQueryParameters); 
    }
}