namespace Core.Geofencing; 

public class MapsUtil {
    private IMapProvider _mapProvider;

    public async Task<string> GetFullRoute(string startPoint, string endPoint) {
        return await _mapProvider.EncodeLocationPoints(startPoint, endPoint); 
    }
}