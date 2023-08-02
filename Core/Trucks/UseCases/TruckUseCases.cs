using Core.Trucks.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.Trucks.UseCases; 

public class TruckUseCases {
    private readonly TrackingGenericRepository _repository;
    
    public TruckUseCases(TrackingGenericRepository repository) {
        _repository = repository;
    }

    public async Task<List<Truck>> ListTrucks(CustomQueryParameters queryParameters) {
        return await _repository.ListTrucks(queryParameters); 
    }

    public async Task<Truck> AddTruck(TruckDto dto) {
        // TODO refactor dto mappers 
        // var truck = TruckMapper.MapDtoToEntity(truckDto);
        var truck = new Truck {
            Id = IdGenerator.Generate(),
            Color = dto.Color,
            Model = dto.Model,
            Status = dto.Status,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        return await _repository.AddTruck(truck); 
    }

    public async Task<TruckDto> GetTruckById(string id) {
        var truck = await _repository.GetTruckById(id);
        var truckDto = TruckMapper.MapEntityToDto(truck);
        return truckDto;
    }
    
    public async Task<bool> DeleteTruck(string id) {
        return await _repository.DeleteTruck(id); 
    }
}