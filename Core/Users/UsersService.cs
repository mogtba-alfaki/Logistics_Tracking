using Core.Users.UseCases.Dto;
using Domain.Entities;
using Infrastructure.Util;

namespace Core.Users.UseCases; 

public class UsersService {
    private readonly UsersUseCases _useCases;

    public UsersService(UsersUseCases useCases) {
        _useCases = useCases;
    }

    public async Task<List<User>> ListUsers(CustomQueryParameters customQueryParameters) {
        return await _useCases.ListUsers(customQueryParameters); 
    }

    public async Task<User> Signup(SignInDto dto) {
        return await _useCases.Signup(dto); 
    }
    
    public async Task<string> Login(UserLoginDto dto) {
        return await _useCases.Login(dto); 
    }
}