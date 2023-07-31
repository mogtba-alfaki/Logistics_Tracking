using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TripRepository {
    private readonly TrackingContext _trackingContext;

    public TripRepository(TrackingContext context) {
        _trackingContext = context;
    } 
    
    public async Task<List<Trip>> ListTrips(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _trackingContext.Trips
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<Trip> GetTripById(string id) {
        var entry = await _trackingContext.Trips.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }
        return entry; 
    }

    public async Task<Trip> AddTrip(Trip trip) {
        var entry =  await _trackingContext.Trips.AddAsync(trip);
        await _trackingContext.SaveChangesAsync();
        return entry.Entity;
    } 
    
    public async Task<bool> DeleteTrip(string id) {
        var entry = await _trackingContext.Trips.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }

        _trackingContext.Trips.Remove(entry);
        await _trackingContext.SaveChangesAsync();
        return true; 
    }

    public async Task<Trip> UpdateTrip(Trip trip) {
        var entry = await _trackingContext.Trips.FindAsync(trip.Id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }

        entry.Destination = trip.Destination;
        entry.FullRoute = trip.FullRoute;
        entry.ShipmentId = trip.ShipmentId;
        entry.Status = trip.Status;
        entry.TruckId = trip.TruckId; 
        entry.UpdatedAt = DateTime.Now.ToUniversalTime();
        await _trackingContext.SaveChangesAsync();
        return entry; 
    }
}