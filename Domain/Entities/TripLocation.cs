using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities; 

public class TripLocation: BaseEntity {
    public string Latitude { get; set; }
    public string Longitude { get; set; } 
    public double Speed { get; set; }
    public int Heading { get; set; }
    public int Altitude { get; set; }
    public string TripId { get; set; }
    public int TimeSpent { get; set; }
    
    public Trip Trip { get; set; }

    public TripLocation(string latitude, string longitude, double speed, int heading, int altitude, string tripId, int timeSpent) {
        Latitude = latitude;
        Longitude = longitude;
        Speed = speed;
        Heading = heading;
        Altitude = altitude;
        TripId = tripId;
        TimeSpent = timeSpent;
    }

    public TripLocation() {
    }
}