using Core.Geofencing;
using Domain.Entities;

namespace Infrastructure.MapProvider; 

public class SpatialDataServices: ISpatialDataServices {
    private readonly GraphHopper _graphHopper = new();
    private readonly NetsuiteGeoApi _netsuiteGeoApi = new(); 
    public async Task<string> EncodeLocationPoints(string startPoint, string endPoint) {
        return await _graphHopper.EncodeLocationPoints(startPoint, endPoint); 
    }

    public async Task<bool> IsLocationWithinPolygon(string polygon, LocationCoordinate locationCoordinate) {
        return await _netsuiteGeoApi.IsLocationInPolygonAsync(polygon, locationCoordinate); 
    }
}