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
    private readonly RestrictedAreaMapper _mapper;
    private readonly ILogger _logger;
    private const string LOGPREFIX = "RestrictedAreaUseCases"; 

    public RestrictedAreasUseCases(IRestrictedAreaRepository repository, ITripRepository tripRepository, ILogger logger, RestrictedAreaMapper mapper) {
        _repository = repository;
        _tripRepository = tripRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<GetRestrictedAreaDto>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        _logger.LogInfo($"{LOGPREFIX}, ListAreas, query: {queryParameters}");
        var areas = await _repository.List(queryParameters);
        return _mapper.MapList(areas); 
    }

    public async Task<GetRestrictedAreaDto> AddRestrictedArea(AddRestrictedArea restrictedAreaDto) {
        _logger.LogInfo($"{LOGPREFIX}, AddRestrictedArea, restrictedARea: {restrictedAreaDto}");
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
        _logger.LogInfo($"{LOGPREFIX}, DeleteRestrictedArea, Id: {restrictedAreaId}");
        return await _repository.Delete(restrictedAreaId); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        _logger.LogInfo($"{LOGPREFIX}, GetRestrictedAreaById, Id: {id}");
        var entry =  await _repository.GetById(id);
        return _mapper.MapEntityToGetterDto(entry);
    }
}