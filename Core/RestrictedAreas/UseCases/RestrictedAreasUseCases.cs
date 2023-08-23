using System.Text.Json;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.UseCases; 

public class RestrictedAreasUseCases {
    private readonly IRestrictedAreaRepository _repository;
    private readonly ITripRepository _tripRepository;
    private readonly ILogger _logger;
    private const string LOGPREFIX = "RestrictedAreaUseCases"; 

    public RestrictedAreasUseCases(IRestrictedAreaRepository repository, ITripRepository tripRepository, ILogger logger) {
        _repository = repository;
        _tripRepository = tripRepository;
        _logger = logger;
    }

    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        _logger.LogInfo($"{LOGPREFIX}, ListAreas, query: {queryParameters}");
        return await _repository.List(queryParameters); 
    }

    public async Task<RestrictedArea> AddRestrictedArea(AddRestrictedArea restrictedAreaDto) {
        _logger.LogInfo($"{LOGPREFIX}, AddRestrictedArea, restrictedARea: {restrictedAreaDto}");
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
        _logger.LogInfo($"{LOGPREFIX}, DeleteRestrictedArea, Id: {restrictedAreaId}");
        return await _repository.Delete(restrictedAreaId); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        _logger.LogInfo($"{LOGPREFIX}, GetRestrictedAreaById, Id: {id}");
        var entry =  await _repository.GetById(id);
        var RestrictedAreaDto = RestrictedAreaMapper.MapEntityToGetterDto(entry);
        return RestrictedAreaDto; 
    }
}