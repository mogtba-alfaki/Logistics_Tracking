using Domain.Entities;

namespace Core.Trips.Dto; 

public class AddTripDto {
    public string Destination { get; set; }
    public int Status { get; set; }
    public string TruckId { get; set; }
    
    public ShipmentDto Shipment { get; set; }
}  