using Domain.Entities;

namespace Core.Geofencing; 

public class SpatialDataUtility {
    private ISpatialDataServices _spatialDataServices;

    public async Task<string> GetEncodedRoute(string startPoint, string endPoint) {
        return await _spatialDataServices.EncodeLocationPoints(startPoint, endPoint); 
    }

    public async Task<bool> IsLocationWithinPolygon(string polygon, LocationCoordinate location) {
        return await _spatialDataServices.IsLocationWithinPolygon(polygon, location); 
    }
}