using AutoMapper;
using Core.Users.UseCases.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.Dto; 

public class UsersMapper {
    private readonly IMapper _autoMapper;

    public UsersMapper(IMapper autoMapper) {
        _autoMapper = autoMapper;
    }

    public UserDto MapEntityToDto(User user) {
        return _autoMapper.Map<UserDto>(user);
    }

    public List<UserDto> MapList(List<User> users) {
        return _autoMapper.Map<List<UserDto>>(users); 
    }
}