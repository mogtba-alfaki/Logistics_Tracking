using System.Text.Json;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.UseCases; 

public class RestrictedAreasUseCases {
    private readonly IRestrictedAreaRepository _repository;
    private readonly ITripRepository _tripRepository;

    public RestrictedAreasUseCases(IRestrictedAreaRepository repository, ITripRepository tripRepository) {
        _repository = repository;
        _tripRepository = tripRepository;
    }

    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        return await _repository.List(queryParameters); 
    }

    public async Task<RestrictedArea> AddRestrictedArea(AddRestrictedArea restrictedAreaDto) {
        var trip = await _tripRepository.GetById(restrictedAreaDto.TripId);
        
        if (trip is null || trip.Status == (int)TripStatuses.ENDED) {
            throw new UnCorrectTripStatusException("Trip is not found or ended"); 
        }
        
        var area = new RestrictedArea {
            Id = IdGenerator.Generate(),
            AreaPolygon = JsonSerializer.Serialize(restrictedAreaDto.AreaPolygon),
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