using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TruckRepository {
    private readonly TrackingContext _context; 
    
    public TruckRepository(TrackingContext context) {
        _context = context;
    }

    public async Task<List<Truck>> ListTrucks() {
        return await _context.Trucks.ToListAsync(); 
    }
}