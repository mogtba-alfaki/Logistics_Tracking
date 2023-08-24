using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Core.Users.UseCases.Dto;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Core.Users.UseCases; 

public class UsersUseCases {
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;
    private const string LOGPREFIX = "UsersUseCase";  
    private readonly UsersMapper _mapper; 

    public UsersUseCases(IUserRepository userRepository, ILogger logger, UsersMapper mapper) {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<UserDto>> ListUsers(CustomQueryParameters customQueryParameters) {
        _logger.LogInfo($"{LOGPREFIX}, ListUsers, query: {customQueryParameters}");
        var users =  await _userRepository.List(customQueryParameters);
        return _mapper.MapList(users); 
    }

        public async Task<UserDto> Signup(SignInDto dto) {
        _logger.LogInfo($"{LOGPREFIX}, Signup, dto: {dto}");
        var hashedPassword = await HashHelper.ComputeHash(dto.Password);
        
        var user = new User {
            Id = IdGenerator.Generate(),
            Username = dto.Username,
            Password = hashedPassword,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            RoleId = dto.RoleId is 0 ? dto.RoleId: (int) UserRoles.USER,
            Token = null,
        };  
        
        var createdUser = await _userRepository.Create(user);
        return _mapper.MapEntityToDto(createdUser); 
    }

    public async Task<string> Login(UserLoginDto dto) {
        _logger.LogInfo($"{LOGPREFIX}, Login, LoginDto: {dto}");
        var username = dto.Username;
        var password = dto.Password; 
        var userExist = await _userRepository.GetByUsername(username);
        var hashedPassword = await HashHelper.ComputeHash(password);
        if (userExist.Password.Trim() != hashedPassword.Trim()) {
            _logger.LogWarn($"{LOGPREFIX}, Login, Wrong Password, LoginDto: {dto}");
            throw new InvalidLoginCredentials("username or password is incorrect"); 
        }

        var token = GenerateJwtToken(userExist);
        return token; 
    }

    // TODO extract this method to a jwt helper class 
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
}