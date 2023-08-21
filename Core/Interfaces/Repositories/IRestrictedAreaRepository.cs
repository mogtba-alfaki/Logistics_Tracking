using Domain.Entities;

namespace Core.Repositories; 

public interface IRestrictedAreaRepository: IBaseRepository<RestrictedArea> {
    public Task<List<RestrictedArea>> GetByTripId(string tripId); 

}