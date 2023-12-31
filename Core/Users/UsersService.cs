using Core.Helpers;
using Core.Users.UseCases;
using Core.Users.UseCases.Dto;
using Domain.Entities;

namespace Core.Users; 

public class UsersService {
    private readonly UsersUseCases _useCases;
 
    public UsersService(UsersUseCases useCases) {
        _useCases = useCases;
    }

    public async Task<List<UserDto>> ListUsers(CustomQueryParameters customQueryParameters) {
        return await _useCases.ListUsers(customQueryParameters); 
    }

    public async Task<UserDto> Signup(SignInDto dto) {
        return await _useCases.Signup(dto); 
    }
    
    public async Task<string> Login(UserLoginDto dto) {
        return await _useCases.Login(dto); 
    }
}