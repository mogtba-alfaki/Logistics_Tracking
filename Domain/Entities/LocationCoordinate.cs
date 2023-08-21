namespace Domain.Entities; 

public class LocationCoordinate: BaseEntity {
    public string Latitude { get; set; }
    public string Longitude { get; set; } 
    public double Speed { get; set; }
    public int Heading { get; set; }
    public int Altitude { get; set; }

    public LocationCoordinate(string latitude, string longitude, double speed, int heading, int altitude) {
        Latitude = latitude;
        Longitude = longitude;
        Speed = speed;
        Heading = heading;
        Altitude = altitude;
    }

    public LocationCoordinate(string latitude, string longitude) {
        Latitude = latitude;
        Longitude = longitude; 
    }
    
    public LocationCoordinate() {
    }

    public override string ToString() {
        return $"""  
        Latitude: {Latitude},
        Longitude: {Longitude},
        Speed: {Speed},
        Heading: {Heading},
        """;
    }
}