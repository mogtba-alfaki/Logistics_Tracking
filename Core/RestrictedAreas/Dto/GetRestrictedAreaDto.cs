namespace Core.RestrictedAreas.Dto; 

public class GetRestrictedAreaDto {
    public string Id { get; set; }
    public string? TripId { get; set; }
    public string AreaPolygon { get; set; }
    public bool? Violated { get; set; }
    public  DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public GetRestrictedAreaDto(string id, string? tripId, string areaPolygon, bool? violated, DateTime createdAt, DateTime updatedAt) {
        Id = id;
        TripId = tripId;
        AreaPolygon = areaPolygon;
        Violated = violated;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public GetRestrictedAreaDto() {}
}