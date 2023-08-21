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
    private readonly RestrictedAreaMapper _mapper;

    public RestrictedAreasUseCases(IRestrictedAreaRepository repository, ITripRepository tripRepository, RestrictedAreaMapper mapper) {
        _repository = repository;
        _tripRepository = tripRepository;
        _mapper = mapper;
    }

    public async Task<List<GetRestrictedAreaDto>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        var areas = await _repository.List(queryParameters);
        return _mapper.MapList(areas); 
    }

    public async Task<GetRestrictedAreaDto> AddRestrictedArea(AddRestrictedArea restrictedAreaDto) {
        var trip = await _tripRepository.GetById(restrictedAreaDto.TripId);
        
        if (trip.Status == (int) TripStatuses.ENDED) {
            throw new UnCorrectTripStatusException("Trip is ended"); 
        }
        
        var area = new RestrictedArea {
            Id = IdGenerator.Generate(),
            AreaPolygon = JsonSerializer.Serialize(restrictedAreaDto.AreaPolygon),
            TripId = restrictedAreaDto.TripId,
            Violated = false,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        };
        
         await _repository.Create(area);
         return _mapper.MapEntityToGetterDto(area); 
    }

    public async Task<bool> DeleteRestrictedArea(string restrictedAreaId) {
        return await _repository.Delete(restrictedAreaId); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        var entry =  await _repository.GetById(id);
        return _mapper.MapEntityToGetterDto(entry);
    }
}