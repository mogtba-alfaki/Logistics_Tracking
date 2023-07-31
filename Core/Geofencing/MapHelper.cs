namespace Core.Geofencing; 

public class MapHelper {
    private readonly IMapProvider _mapProvider;

    public MapHelper(IMapProvider mapProvider) {
        _mapProvider = mapProvider;
    }

    public async Task<string> GetFullRoute(string startPoint, string endPoint) {
        return await _mapProvider.EncodeLocationPoints(startPoint, endPoint); 
    }
}