using Core.Exceptions;
using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories; 


// ** Refactor this to use the generic repository and implement the interface 
public class  TripRepository: BaseRepository<Trip>, ITripRepository {
    private readonly TrackingContext _context;

    public TripRepository(TrackingContext context) : base(context) {
        _context = context;
    }

    public override async Task<Trip> Update(Trip trip) {
        var entry = await _context.Trips.FindAsync(trip.Id);
        if (entry is null) {
            throw new NotFoundException("Not Found"); 
        }
        
        entry.Destination = trip.Destination;
        entry.FullRoute = trip.FullRoute;
        entry.ShipmentId = trip.ShipmentId;
        entry.Status = trip.Status;
        entry.TruckId = trip.TruckId; 
        entry.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        await _context.SaveChangesAsync();
        return entry; 
    }
}