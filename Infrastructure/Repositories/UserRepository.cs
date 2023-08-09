using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class UserRepository {
    private readonly TrackingContext _context;
    public UserRepository(TrackingContext context) {
        _context = context;
    }

    public async Task<List<User>> ListUsers(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _context.Users
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<User>  AddUser(User user) {
        var result = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(); 
        return result.Entity; 
    }

    public async Task<User> GetByUsername(string username) {
        var result = await _context.Users
            .FirstOrDefaultAsync(user => user.Username == username);
        if (result is null) {
            throw new Exception("Not Found"); 
        }
        return result;
    }
}