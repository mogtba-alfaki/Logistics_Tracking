using Domain.Entities;

namespace Core.Repositories; 

public interface ITripLocationRepository: IBaseRepository<TripLocation> {
    public Task<List<TripLocation>> GetTripLocationsByTripId(string tripId);
    public Task<TripLocation> GetLatestTripLocation(string tripId); 
}