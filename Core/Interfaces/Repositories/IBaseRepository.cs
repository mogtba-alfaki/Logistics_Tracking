using Core.Helpers;
namespace Core.Repositories; 

public interface IBaseRepository<T> {
    public Task<List<T>> List(CustomQueryParameters customQueryParameters); 
    
    public Task<T> Create(T entity); 

    public Task<T> GetById(string id);

    public Task<bool> Delete(string id);

    public Task<T> Update(T entity);
}