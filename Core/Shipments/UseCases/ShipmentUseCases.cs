using Core.Trips.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.Shipments.UseCases; 

public class ShipmentUseCases {
    private readonly TrackingGenericRepository _repository;

    public ShipmentUseCases(TrackingGenericRepository repository) {
        _repository = repository;
    } 
    
    
    public async Task<List<Shipment>> ListShipments(CustomQueryParameters options) {
        return await _repository.ListShipments(options); 
    }

    public async Task<Shipment> GetShipmentById(string id) {
        return await _repository.GetShipmentById(id); 
    }

    public async Task<Shipment> AddShipment(AddShipmentDto shipment) {
        var Shipment = new Shipment {
            Breakable = shipment.Breakable,
            StorageTemperature = shipment.StorageTemperature,
            Id = IdGenerator.Generate(),
            Quantity = shipment.Quantity,
            QuantityMeasure = shipment.QuantityMeasure,
            Weight = shipment.Weight,
            Type = shipment.Type,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        return await _repository.AddShipment(Shipment); 
    } 
    
    public async Task<bool> DeleteShipment(string id) {
        return await _repository.DeleteShipment(id); 
    }
}