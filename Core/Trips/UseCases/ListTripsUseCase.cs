using Core.Helpers;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Core.Trips.Dto;
using Domain.Entities;

namespace Core.Trips.UseCases; 

public class ListTripsUseCase {
    private readonly ITripRepository _repository;
    private readonly TripsMapper _mapper;

    public ListTripsUseCase(ITripRepository repository, TripsMapper mapper) {
    public ListTripsUseCase(ITripRepository repository) {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetTripDto>> ListTrips(CustomQueryParameters customQueryParameters) {
        var trips = await _repository.List(customQueryParameters);
        return _mapper.MapList(trips); 
    }
}