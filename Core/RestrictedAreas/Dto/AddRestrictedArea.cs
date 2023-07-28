namespace Core.RestrictedAreas.Dto; 

public class AddRestrictedArea {
    public string? TripId { get; set; }
    public string AreaPolygon { get; set; }
    public bool? Violated { get; set; }
}