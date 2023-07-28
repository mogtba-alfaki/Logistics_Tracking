using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class RestrictedAreaRepository {
    private readonly TrackingContext _context;

    public RestrictedAreaRepository(TrackingContext context) {
        _context = context;
    }
    
    public async Task<List<RestrictedArea>> ListRestrictedAreas() {
        return await _context.RestrictedAreas.ToListAsync(); 
    }
}