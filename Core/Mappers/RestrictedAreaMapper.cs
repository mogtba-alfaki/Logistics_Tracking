using AutoMapper;
using Domain.Entities;

namespace Core.RestrictedAreas.Dto;

public class RestrictedAreaMapper {
    private  readonly IMapper _mapper;

    public RestrictedAreaMapper(IMapper mapper) {
        _mapper = mapper; 
    }

    public RestrictedArea MapGetterDtoToEntity(GetRestrictedAreaDto getRestrictedAreaDtoDto) {
        return _mapper.Map<RestrictedArea>(getRestrictedAreaDtoDto);
    }

    public GetRestrictedAreaDto MapEntityToGetterDto(RestrictedArea restrictedArea) {
        return new GetRestrictedAreaDto {
            Id = restrictedArea.Id,
            Violated = restrictedArea.Violated,
            TripId = restrictedArea.TripId,
            CreatedAt = restrictedArea.CreatedAt,
            UpdatedAt = restrictedArea.UpdatedAt,
        };
    }

    public AddRestrictedArea MapEntityToAddRestrictedAreaDto(RestrictedArea restrictedArea) {
        return _mapper.Map<AddRestrictedArea>(restrictedArea);
    }

    public List<GetRestrictedAreaDto> MapList(List<RestrictedArea> areas) {
        return _mapper.Map<List<GetRestrictedAreaDto>>(areas); 
    }
}