using Domain.Entities;

namespace Core.Repositories;

public interface ITruckRepository : IBaseRepository<Truck> {
    public Task<bool> ChangeTruckStatus(string id, int newStatus); 
}