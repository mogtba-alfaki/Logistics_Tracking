using System.Text.Json;
using Core.Exceptions;
using Domain.Entities;
using NetTopologySuite; 
using NetTopologySuite.Geometries; 

namespace Infrastructure.GeoApisProviders; 

public class NetsuiteGeoApi {
    private const int Srid = 4326;
    private  readonly  GeometryFactory _geometryFactory =
        NtsGeometryServices.Instance.CreateGeometryFactory(Srid);
    
    public async Task<bool> IsLocationInPolygonAsync(string polygon, LocationCoordinate coordinate) {
        var polygonCoords = extractPolygonCoordinates(polygon); 
        var locationPoint = new Coordinate(
        Convert.ToDouble(coordinate.Latitude) , Convert.ToDouble(coordinate.Longitude));
        
        var geometryPolygon = _geometryFactory.CreatePolygon(polygonCoords.ToArray());
        var point = _geometryFactory.CreatePoint(locationPoint);

        return point.Within(geometryPolygon);
    }

    public async Task<bool> IsValidGeometryPolygon(string areaPolygon) {
        var polygonCoords = extractPolygonCoordinates(areaPolygon);
        try {
            var geometryPolygon = _geometryFactory.CreatePolygon(polygonCoords.ToArray());
            return geometryPolygon.IsValid; 
        }
        catch (Exception e) {
            throw new SpatialDataApisException(e.Message); 
        }
    }

    private List<Coordinate> extractPolygonCoordinates(string polygon) {
        var polygonObject = JsonSerializer.Deserialize<List<List<double>>>(polygon);
        List<Coordinate> polygonCoords = new(); 
        
        foreach (var p in polygonObject) {
            var latitude = p[0];
            var longitude = p[1];
            polygonCoords.Add(new Coordinate(latitude, longitude)); 
        }

        return polygonCoords; 
    }
}