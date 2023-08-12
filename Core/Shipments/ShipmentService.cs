using Core.Helpers;
using Core.Shipments.UseCases;
using Core.Trips.Dto;
using Domain.Entities;

namespace Core.Shipments; 

public class ShipmentService {
    private readonly ShipmentUseCases _useCases;

    public ShipmentService(ShipmentUseCases useCases) {
        _useCases = useCases;
    } 
    
    public async Task<List<Shipment>> ListShipments(CustomQueryParameters options) {
        return await _useCases.ListShipments(options); 
    }

    public async Task<Shipment> GetShipmentById(string id) {
        return await _useCases.GetShipmentById(id); 
    }

    public async Task<Shipment> AddShipment(AddShipmentDto shipment) {
        
        return await _useCases.AddShipment(shipment); 
    } 
    
    public async Task<bool> DeleteShipment(string id) {
        return await _useCases .DeleteShipment(id); 
    }
}