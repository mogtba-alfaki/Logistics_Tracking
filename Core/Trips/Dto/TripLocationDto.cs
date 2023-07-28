namespace Core.Trips.Dto; 

public class TripLocationDto {
    public string Latitude { get; set; }
    public string Longitude { get; set; } 
    public double Speed { get; set; }
    public int Heading { get; set; }
    public int Altitude { get; set; }
    public string TripId { get; set; }
    public int TimeSpent { get; set; }

    public override string ToString() {
        return $""" 
        Latitude: {Latitude},
        Longitude: {Longitude},
        """; 
    }
}