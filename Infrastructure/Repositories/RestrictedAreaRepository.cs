using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class RestrictedAreaRepository: BaseRepository<RestrictedArea>, IRestrictedAreaRepository {
    public RestrictedAreaRepository(TrackingContext context) : base(context) {
    }
}