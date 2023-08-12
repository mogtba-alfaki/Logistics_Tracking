using Core.Helpers;
using Core.Trucks.Dto;
using Core.Trucks.UseCases;
using Domain.Entities;

namespace Core.Trucks; 

public class TrucksService {
    private readonly TruckUseCases _truckUseCases;

    public TrucksService(TruckUseCases truckUseCases) {
        _truckUseCases = truckUseCases;
    }

    public async Task<List<Truck>> ListTrucks(CustomQueryParameters options) {
        return await _truckUseCases.ListTrucks(options); 
    }
    
    public async Task<Truck> AddTruck(TruckDto truckDto) {
        return await _truckUseCases.AddTruck(truckDto); 
    }

    public async Task<bool> DeleteTruck(string id) {
        return await _truckUseCases.DeleteTruck(id); 
    }

    public async Task<TruckDto> GetTruckById(string id) {
        return await _truckUseCases.GetTruckById(id); 
    }
}