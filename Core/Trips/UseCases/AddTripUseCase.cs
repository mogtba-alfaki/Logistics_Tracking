using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Core.Trips.Dto;
using Domain.Entities;

namespace Core.Trips.UseCases; 

public class AddTripUseCase {
    private readonly ITripRepository _tripRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly ITruckRepository _truckRepository;
    private readonly TripsMapper _mapper;

    public AddTripUseCase(ITripRepository tripRepository, IShipmentRepository shipmentRepository, ITruckRepository truckRepository, TripsMapper mapper) {
        _tripRepository = tripRepository;
        _shipmentRepository = shipmentRepository;
        _truckRepository = truckRepository;
        _mapper = mapper;
    }

    public async Task<GetTripDto> AddTrip(AddTripDto dto) {
        var truck = await _truckRepository.GetById(dto.TruckId);
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
        
       var Shipment =  await _shipmentRepository.Create(tripShipment); 
        
        var trip = new Trip {
            Id = IdGenerator.Generate(),
            Destination = dto.Destination,
            Status = (int)TripStatuses.STARTED,
            TruckId = dto.TruckId,
            ShipmentId = Shipment.Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        };
        
        var result = await _tripRepository.Create(trip);
        await _truckRepository.ChangeTruckStatus(truck.Id,(int) TruckStatuses.ON_TRIP);
        return _mapper.MapToDto(trip);
    }
}