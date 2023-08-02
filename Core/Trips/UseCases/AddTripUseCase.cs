using Core.Enums;
using Core.Exceptions;
using Core.Trips.Dto;
using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Util;

namespace Core.Trips.UseCases; 

public class AddTripUseCase {
    private readonly TripRepository _tripRepository;
    private readonly TrackingGenericRepository _trackingGenericRepository; 
    
    public AddTripUseCase(TripRepository tripRepository, TrackingGenericRepository trackingGenericRepository) {
        _tripRepository = tripRepository;
        _trackingGenericRepository = trackingGenericRepository; 
    }

    public async Task<Trip> AddTrip(AddTripDto dto) {
        var truck = await _trackingGenericRepository.GetTruckById(dto.TruckId);
        if (truck == null ||  truck.Status == (int) TruckStatuses.ON_TRIP) {
            throw new UnCorrectTruckStatusException("Truck is Already on Trip"); 
        }

        var tripShipment = new Shipment {
            Breakable = dto.Shipment.Breakable,
            StorageTemperature = dto.Shipment.StorageTemperature,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
            Id = IdGenerator.Generate(),
            Quantity = dto.Shipment.Quantity,
            QuantityMeasure = dto.Shipment.QuantityMeasure,
            Weight = dto.Shipment.Weight,
            Type = dto.Shipment.Type,
        }; 
        
       var Shipment =  await _trackingGenericRepository.AddShipment(tripShipment); 
        
        var trip = new Trip {
            Id = IdGenerator.Generate(),
            Destination = dto.Destination,
            Status = (int)TripStatuses.STARTED,
            TruckId = dto.TruckId,
            ShipmentId = Shipment.Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        };
        
        var result = await _tripRepository.AddTrip(trip);
        await _trackingGenericRepository.ChangeTruckStatus(truck.Id,(int) TruckStatuses.ON_TRIP);
        return result;
    }
}