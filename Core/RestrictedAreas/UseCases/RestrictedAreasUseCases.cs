using Core.Helpers;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.UseCases; 

public class RestrictedAreasUseCases {
    private readonly IRestrictedAreaRepository _repository;

    public RestrictedAreasUseCases(IRestrictedAreaRepository repository) {
        _repository = repository;
    }

    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        return await _repository.List(queryParameters); 
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
        return await _repository.Create(area); 
    }

    public async Task<bool> DeleteRestrictedArea(string restrictedAreaId) {
        return await _repository.Delete(restrictedAreaId); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        var entry =  await _repository.GetById(id);
        var RestrictedAreaDto = RestrictedAreaMapper.MapEntityToGetterDto(entry);
        return RestrictedAreaDto; 
    }
}