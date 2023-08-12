using Domain.Entities;

namespace Core.Repositories;

public interface IUserRepository : IBaseRepository<User> {
    public Task<User> GetByUsername(string username); 
}