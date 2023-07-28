using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TripLocationsRepository {
    private readonly TrackingContext _trackingContext;

    public TripLocationsRepository(TrackingContext trackingContext) {
        _trackingContext = trackingContext;
    }

    public async Task<TripLocation> AddTripLocation(TripLocation location) {
        var result = await  _trackingContext.TripLocations.AddAsync(location);
            await _trackingContext.SaveChangesAsync(); 
            return result.Entity;        
    }

    public async Task<List<TripLocation>> GetTripLocationsByTripId(string tripId) {
        var locations = await _trackingContext.TripLocations 
            .Where(x => x.TripId == tripId).ToListAsync();
        return locations; 
    }
}