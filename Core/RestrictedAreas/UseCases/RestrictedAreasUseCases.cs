using Core.RestrictedAreas.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.RestrictedAreas.UseCases; 

public class RestrictedAreasUseCases {
    private readonly TrackingGenericRepository _repository;

    public RestrictedAreasUseCases(TrackingGenericRepository repository) {
        _repository = repository;
    }

    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        return await _repository.ListRestrictedAreas(queryParameters); 
    }

    public async Task<RestrictedArea> AddRestrictedArea(AddRestrictedArea restrictedAreaDto) {
        var area = new RestrictedArea {
            Id = IdGenerator.Generate(),
            AreaPolygon = restrictedAreaDto.AreaPolygon,
            TripId = restrictedAreaDto.TripId,
            Violated = false,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        };
        return await _repository.AddRestrictedArea(area); 
    }

    public async Task<bool> DeleteRestrictedArea(string restrictedAreaId) {
        return await _repository.DeleteRestrictedArea(restrictedAreaId); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        var entry =  await _repository.GetRestrictedAreaById(id);
        var RestrictedAreaDto = RestrictedAreaMapper.MapEntityToGetterDto(entry);
        return RestrictedAreaDto; 
    }
}