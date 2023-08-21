using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TripLocationsRepository:  BaseRepository<TripLocation>, ITripLocationRepository {
    private readonly TrackingContext _context;

    public TripLocationsRepository(TrackingContext context) : base(context) {
        _context = context;
    }

    public async Task<List<TripLocation>> GetTripLocationsByTripId(string tripId) {
        var locations = await _context.TripLocations 
            .Where(x => x.TripId == tripId).ToListAsync();
        return locations; 
    }

    public async Task<TripLocation> GetLatestTripLocation(string tripId) {
        var location = await _context.TripLocations
            .Where(x => x.TripId == tripId)
            .OrderBy(x => x.CreatedAt)
            .LastAsync();
        return location; 
    }
}