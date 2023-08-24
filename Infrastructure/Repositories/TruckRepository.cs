using Core.Exceptions;
using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories; 

public class TruckRepository: BaseRepository<Truck>, ITruckRepository {
    private readonly TrackingContext _context;

    public TruckRepository(TrackingContext context) : base(context) {
        _context = context;
    }

    public async Task<bool> ChangeTruckStatus(string id, int newStatus) {
        var truck = await _context.Trucks.FindAsync(id);
        if (truck is null) {
            throw new NotFoundException("Not Found"); 
        }

        truck.Status = newStatus;
        await _context.SaveChangesAsync();
        return true;
    }

    public override async Task<Truck> Update(Truck truck) {
        var entry = await _context.Trucks.FindAsync(truck.Id);
        if (entry is null) {
            throw new NotFoundException("Not Found"); 
        }

        entry.Color = truck.Color;
        entry.Model = truck.Model;
        entry.Status = truck.Status;
        entry.ImageStorageId = truck.ImageStorageId;
        entry.UpdatedAt = DateTime.Now.ToUniversalTime();

        await _context.SaveChangesAsync();
        return entry; 
    }
}