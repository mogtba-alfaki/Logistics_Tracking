namespace Domain.Entities; 

public class RestrictedArea: BaseEntity {
    public string? TripId { get; set; }
    public string AreaPolygon { get; set; }
    public bool? Violated { get; set; }
    
    public Trip Trip { get; set; }
    
    public RestrictedArea(string? tripId, string areaPolygon, bool? violated) {
        TripId = tripId;
        AreaPolygon = areaPolygon;
        Violated = violated;
    }

    public RestrictedArea() {
    }
}


