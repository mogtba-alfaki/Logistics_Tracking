using Microsoft.AspNetCore.Http;

namespace Core.Interfaces; 

public interface IObjectStorageProvider {
    public Task<string> UploadImageAsync(string imagePath);
    
    public Task<GetImageResponse> GetImageAsync(string imageId);
    
    public Task<bool> DeleteImageFromBucket(string imageId); 
}

public class GetImageResponse {
}