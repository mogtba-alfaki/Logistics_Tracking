using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities; 

public class Trip: BaseEntity { 
    public string Destination { get; set; }
    
    public List<RestrictedArea>? RestrictedAreas { get; set; }
    
    public string? FullRoute { get; set; }
    public int Status { get; set; }
    public string TruckId { get; set; }
    public string ShipmentId { get; set; }
    public Truck Truck { get; set; }
    
    public Shipment Shipment { get; set; }
    
    public List<TripLocation> TripLocations { get; set; }
    
    

    public Trip(string destination, List<RestrictedArea> restrictedAreas, string? fullRoute, int status, string truckId, string shipmentId, Truck truck, Shipment shipment) {
        Destination = destination;
        RestrictedAreas = restrictedAreas;
        FullRoute = fullRoute;
        Status = status;
        TruckId = truckId;
        ShipmentId = shipmentId;
        Truck = truck;
        Shipment = shipment;
    }

    public Trip() {
    }

    public override string ToString() {
        return $""" 
            TruckId {TruckId} 
            Destination {Destination},
            ShipmentId {ShipmentId},
            RestrictedAreas  {RestrictedAreas},
            FullRoute {FullRoute} 
            Status {Status}
        """; 
    }
}