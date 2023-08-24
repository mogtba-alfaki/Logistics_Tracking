using Core.Exceptions;
using Core.Helpers;
using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity {
    private readonly TrackingContext _context;

    public BaseRepository(TrackingContext context) {
        _context = context;
    }

    public async Task<List<T>> List(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _context.Set<T>()
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<T> Create(T entity) {
        var result = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity; 
    }

    public async Task<T> GetById(string id) {
       var entity =   await _context.Set<T>().FindAsync(id);
       if (entity is null) {
           throw new NotFoundException("Not Found");
       }
       return entity;
    }

    public async Task<bool> Delete(string id) {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity is null) {
            throw new NotFoundException("Not Found"); 
        } 
        
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return true; 
    }

    public virtual  async Task<T> Update(T entity) {
        var entityFound = await _context.Set<T>().FindAsync(entity.Id);
        if (entityFound is null) {
            throw new NotFoundException("Not Found"); 
        }

        var newEntity  = _context.Set<T>().Update(entityFound);
        return newEntity.Entity;
    }
}