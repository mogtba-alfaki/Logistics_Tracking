using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;

namespace Infrastructure.Repositories; 

public class ShipmentRepository: BaseRepository<Shipment>, IShipmentRepository {
    private readonly TrackingContext _context;  
    
    public ShipmentRepository(TrackingContext context) : base(context) {
    }

    public override async Task<Shipment> Update(Shipment shipment) {
        var entry = await _context.Shipments.FindAsync(shipment.Id);
        if (entry is null) {
            throw new Exception("Not Found");  
        }

        entry.Breakable = shipment.Breakable;
        entry.Quantity = shipment.Quantity;
        entry.Type = shipment.Type;
        entry.Weight = shipment.Weight;
        entry.QuantityMeasure = shipment.QuantityMeasure;
        entry.StorageTemperature = shipment.StorageTemperature;
        entry.UpdatedAt = DateTime.Now.ToUniversalTime();

        await _context.SaveChangesAsync();
        return entry; 
    }
    
}