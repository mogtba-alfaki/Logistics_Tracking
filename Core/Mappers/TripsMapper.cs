using AutoMapper;
using Core.Trips.Dto;
using Domain.Entities;

namespace Core.RestrictedAreas.Dto; 

public class TripsMapper {
    private readonly IMapper _autoMapper;

    public TripsMapper(IMapper autoMapper) {
        _autoMapper = autoMapper;
    }

    public GetTripDto MapToDto(Trip trip) {
        return _autoMapper.Map<GetTripDto>(trip); 
    }
    
    public List<GetTripDto> MapList(List<Trip> trips) {
        return _autoMapper.Map<List<GetTripDto>>(trips); 
    }
}