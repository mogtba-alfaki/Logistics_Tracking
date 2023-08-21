using AutoMapper;
using Core.Trucks.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.Dto; 

public class TrucksMapper {
    private readonly IMapper _autoMapper;

    public TrucksMapper(IMapper autoMapper) {
        _autoMapper = autoMapper;
    }

    public TruckDto MapEntityToDto(Truck truck) {
        return _autoMapper.Map<TruckDto>(truck); 
    }

    public List<TruckDto> MapList(List<Truck> trucks) {
        return _autoMapper.Map<List<TruckDto>>(trucks); 
    }
}