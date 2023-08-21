using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class RestrictedAreaRepository: BaseRepository<RestrictedArea>, IRestrictedAreaRepository {
    private readonly TrackingContext _context;

    public RestrictedAreaRepository(TrackingContext context) : base(context) {
        _context = context;
    }

    public async Task<List<RestrictedArea>> GetByTripId(string tripId) {
        return await _context.RestrictedAreas
            .Where(area => area.TripId == tripId).ToListAsync();
    }
}