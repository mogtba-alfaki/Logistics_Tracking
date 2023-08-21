namespace Core.Geofencing; 

public static class MapProviderConstants {
    public static string GraphHopperRoutesApiUrl = Environment.GetEnvironmentVariable("GRAPHHOPPER_URL");
    public static string GraphHopperApiKey = Environment.GetEnvironmentVariable("GRAPHHOPPER_API_KEY"); 
}