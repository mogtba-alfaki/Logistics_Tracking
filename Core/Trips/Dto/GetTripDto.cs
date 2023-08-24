using Domain.Entities;

namespace Core.Trips.Dto; 

public class GetTripDto {
    
    public string Destination { get; set; }
    public List<RestrictedArea>? RestrictedAreas { get; set; }
    public string? FullRoute { get; set; }
    public int Status { get; set; }
    public string TruckId { get; set; }
    public string ShipmentId { get; set; }
    public Truck Truck { get; set; }
    public Shipment Shipment { get; set; }
    public List<TripLocation> TripLocations { get; set; }

    public GetTripDto(string destination, string? fullRoute, int status, string truckId, string shipmentId) {
        Destination = destination;
        FullRoute = fullRoute;
        Status = status;
        TruckId = truckId;
        ShipmentId = shipmentId;
    }
}