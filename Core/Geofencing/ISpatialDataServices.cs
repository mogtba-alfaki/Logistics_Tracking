using Domain.Entities;

namespace Core.Geofencing; 

public interface ISpatialDataServices {
    public Task<string> EncodeLocationPoints(string startPoint, string endPoint);
    public Task<bool> IsLocationWithinPolygon(string polygon, LocationCoordinate locationCoordinate);
    
}