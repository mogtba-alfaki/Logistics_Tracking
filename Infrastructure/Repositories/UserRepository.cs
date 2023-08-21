using Core.Helpers;
using Core.Repositories;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class UserRepository: BaseRepository<User>, IUserRepository {
    private readonly TrackingContext _context;

    public UserRepository(TrackingContext context) : base(context) {
        _context = context;
    }

    public async Task<User> GetByUsername(string username) {
        var result = await _context.Users
            .FirstOrDefaultAsync(user => user.Username == username);
        if (result is null) {
            throw new Exception("Not Found"); 
        }
        return result;
    }

    public override async Task<User> Update(User user) {
        var userFound = await _context.Users.FindAsync(user.Id);
        if (userFound is null) {
            throw new Exception("Not Found"); 
        }

        userFound.Password = user.Password;
        userFound.Username = user.Username;
        userFound.Token = user.Token;
        userFound.RoleId = user.RoleId;
        userFound.UpdatedAt = DateTime.Now.ToUniversalTime();

        await _context.SaveChangesAsync();
        return userFound;
    }
}