using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Enums;
using Core.Exceptions;
using Core.Users.UseCases.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;
using Microsoft.IdentityModel.Tokens;

namespace Core.Users.UseCases; 

public class UsersUseCases {
    private readonly UserRepository _userRepository;

    public UsersUseCases(UserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<List<User>> ListUsers(CustomQueryParameters customQueryParameters) {
        return await _userRepository.ListUsers(customQueryParameters); 
    }

    public async Task<User> Signup(SignInDto dto) {
        var hashedPassword = GenerateHash(dto.Password);
        var u = new User {
            Id = IdGenerator.Generate(),
            Username = dto.Username,
            Password = hashedPassword,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            RoleId = dto.RoleId is 0 ? dto.RoleId: (int) UserRoles.USER,
            token = null,
        };  
        
        return await _userRepository.AddUser(u); 
    }

    public async Task<string> Login(string username, string password) {
        var userExist = await _userRepository.GetByUsername(username);
        // TODO SHOULD HASH THE PASSWORD
        if (userExist.Password != password) {
            throw new InvalidLoginCredentials("username or password is incorrect"); 
        }

        var token = GenerateJwtToken(userExist);
        return token; 
    }

    private string GenerateJwtToken(User user) {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");  
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); 
        var claims = new Claim[] {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.RoleId.ToString()), 
        };

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }

    private string GenerateHash(string text) {
        HashAlgorithm algorithm = SHA512.Create();
        byte[] bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(bytes); 
    }
}