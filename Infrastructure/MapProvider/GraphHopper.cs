using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Geofencing; 

public class GraphHopper: IMapProvider {
    private readonly HttpClient client = new HttpClient();
    private string testingPoint1 = "15.666470000000,32.627213333333";
    private string testingPoint2 = "15.609442000000,32.568962000000"; 
    
    public async Task<string> EncodeLocationPoints(string startPoint, string endPoint) {
        
        // just for testing   
        if (startPoint == "" && endPoint == "") {
            startPoint = testingPoint1;
            endPoint = testingPoint2; 
        } 
        
        var apiKey = MapProviderConstants.GraphHopperApiKey; 
        var routeUrl = MapProviderConstants.GraphHopperRoutesApiUrl +
                       "vehicle=car" +
                       "&weighting=fastest" +
                       // "&point=15.666470000000,32.627213333333 +
                       // $"&point=15.609442000000,32.568962000000 +
                       $"&point={startPoint}" +
                       $"&point={endPoint}" +
                       $"&locale=en" +
                       $"&points_encoded=true" +
                       $"&key={apiKey}";
        Console.WriteLine(routeUrl);
        var ghResponse = await fetchGraphHooper(routeUrl);
        return ghResponse.Paths[0].Points; 
    }

    private async Task<GraphHopperResponse>  fetchGraphHooper(string url) {
        try {
            var result = await client.GetStringAsync(url);
            GraphHopperResponse response = 
                JsonSerializer.Deserialize<GraphHopperResponse>(result);
            Console.WriteLine(response.Paths[0].Points);
            return response; 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }
}




