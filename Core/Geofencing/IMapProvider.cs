namespace Core.Geofencing; 

public interface IMapProvider {
    public Task<string> EncodeLocationPoints(string startPoint, string endPoint);
}