using System.Text.Json;
using Domain.Entities;
using NetTopologySuite; 
using NetTopologySuite.Geometries; 

namespace Infrastructure.MapProvider; 

public class NetsuiteGeoApi {
    
    public async Task<bool> IsLocationInPolygonAsync(string polygon, LocationCoordinate coordinate) {
        var polygonObject = JsonSerializer.Deserialize<List<List<double>>>(polygon);
        List<Coordinate> polygonCoords = new(); 
        
        foreach (var p in polygonObject) {
            var latitude = p[0];
            var longitude = p[1];
            polygonCoords.Add(new Coordinate(latitude, longitude)); 
        } 
        
        var locationPoint = new Coordinate(
        Convert.ToDouble(coordinate.Latitude) , Convert.ToDouble(coordinate.Longitude));
        
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
        var geometryPolygon = geometryFactory.CreatePolygon(polygonCoords.ToArray());
        var point = geometryFactory.CreatePoint(locationPoint);

        return point.Within(geometryPolygon);
    }
}