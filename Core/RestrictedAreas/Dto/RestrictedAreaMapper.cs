using Domain.Entities;

namespace Core.RestrictedAreas.Dto; 

public class RestrictedAreaMapper {
    public static RestrictedArea MapGetterDtoToEntity(GetRestrictedAreaDto getRestrictedAreaDtoDto) {
        return new RestrictedArea {
            Id = getRestrictedAreaDtoDto.Id,
            Violated = getRestrictedAreaDtoDto.Violated,
            TripId = getRestrictedAreaDtoDto.TripId,
            CreatedAt = getRestrictedAreaDtoDto.CreatedAt,
            UpdatedAt = getRestrictedAreaDtoDto.UpdatedAt,
        }; 
    }

    public static GetRestrictedAreaDto MapEntityToGetterDto(RestrictedArea restrictedArea) {
        return new GetRestrictedAreaDto {
            Id = restrictedArea.Id,
            Violated = restrictedArea.Violated,
            TripId = restrictedArea.TripId,
            CreatedAt = restrictedArea.CreatedAt,
            UpdatedAt = restrictedArea.UpdatedAt,
        };
    }
}